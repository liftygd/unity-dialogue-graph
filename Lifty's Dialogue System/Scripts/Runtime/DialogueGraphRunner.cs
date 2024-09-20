using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphRunner : MonoBehaviour
    {
        //Events
        public event Action OnFileLoaded;
        public event Action OnDialogueStarted;
        public event Action OnDialogueEnded;
        
        [SerializeField] protected DialogueGraphAsset _dialogueGraph;
        [SerializeField] protected DialogueFileData _dialogueFile;
        [SerializeField] protected bool _runOnStart;

        protected bool _dialogueRunning;
        protected Dictionary<string, DialogueTextData> _dialogueText;
        protected DialogueGraphNode _currentNode;

        [Header("UI")]
        [SerializeField] protected List<DialogueCharacterBubbleBase> _characterBubbles;
        protected DialogueCharacterBubbleBase _currentBubble;

        [Header("Events")] 
        [SerializeField] protected List<DialogueGraphEvent> _events;

        protected virtual void Start()
        {
            LoadFile(DialogueGraphController.Instance.CurrentDialogueLanguage);
            DialogueGraphController.Instance.DialogueLanguageChanged += LoadFile;
            
            if (_dialogueGraph == null) return;
            
            _dialogueGraph.Nodes.ForEach(node => node.Configurate());
            if (_runOnStart) StartDialogue();
        }

        protected virtual void OnDestroy()
        {
            DialogueGraphController.Instance.DialogueLanguageChanged -= LoadFile;
            
            if (_dialogueGraph != null)
                _dialogueGraph.Nodes.ForEach(node => node.Dispose());
        }

        #region Dialogue

        public virtual bool IsRunning()
        {
            return _dialogueRunning;
        }

        public virtual void StartDialogue()
        {
            if (_dialogueRunning) return;
            
            _dialogueRunning = true;
            var startNode = GetStartNode();
            startNode.Process(this);
            OnDialogueStarted?.Invoke();
        }

        public virtual void StartDialogue(DialogueGraphNode node)
        {
            if (_dialogueRunning) return;
            if (node == null) return;
            
            _dialogueRunning = true;
            node.Process(this);
            OnDialogueStarted?.Invoke();
        }

        public DialogueNode_Start GetStartNode()
        {
            return _dialogueGraph.GetStartNode();
        }

        public DialogueGraphNode GetNodeByID(string guid)
        {
            return _dialogueGraph.GetNodeByID(guid);
        }

        public virtual void EndDialogue()
        {
            HideBubble();
            OnDialogueEnded?.Invoke();

            _currentBubble = null;
            _dialogueRunning = false;
            _currentNode = null;
        }

        public virtual void SetCurrentNode(DialogueGraphNode node)
        {
            _currentNode = node;
        }

        public void HideBubble()
        {
            if (_currentBubble != null)
                _currentBubble.Hide();
        }

        public void DelayCallback(float time, Action callback)
        {
            StartCoroutine(Delay(time, callback));
        }

        private IEnumerator Delay(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            
            callback?.Invoke();
        }

        public void CallEvent(string eventID)
        {
            var foundEvent = _events.First(e => e.EventID == eventID);

            if (foundEvent == null)
            {
                Debug.LogError("DIALOGUE GRAPH: Trying to call event by ID, but it does not exist.");
                return;
            }
            
            foundEvent.CallEvent();
        }

        #endregion

        #region Variables

        public T GetVariableValue<T>(string variableName)
        {
            return DialogueGraphController.Instance.GetVariableValue<T>(variableName);
        }

        public void SetVariableValue<T>(string variableName, T variableValue)
        {
            DialogueGraphController.Instance.SetVariableValue<T>(variableName, variableValue);
        }

        #endregion

        #region TextData
        
        private void LoadFile(DialogueLanguageEnum language)
        {
            if (_dialogueFile == null) return;

            _dialogueText = new Dictionary<string, DialogueTextData>();
            var textFile = _dialogueFile.GetFileByLanguage(language);
            
            DialogueTextData[] textData = JsonHelper.FromJson<DialogueTextData>(textFile.DialogueTextFile.text);
            foreach (var data in textData)
            {
                _dialogueText.Add(data.PhraseID, data);
            }
            
            OnFileLoaded?.Invoke();
        }

        //Show text data by phrase ID
        public void ShowTextData(string phraseID, Action callback)
        {
            var textData = GetTextData(phraseID);
            ShowText(textData, callback);
        }

        //Show text data from class
        public void ShowTextData(DialogueTextData textData, Action callback)
        {
            ShowText(textData, callback);
        }

        //Show text data with variable replacement
        public void ShowTextData(string phraseID, Action callback, DialogueGraphVariable<object>[] variablesToReplace)
        {
            var textData = new DialogueTextData(GetTextData(phraseID));

            foreach (var variable in variablesToReplace)
            {
                textData.Phrase = textData.Phrase.Replace(variable.Name, variable.Value.ToString());
            }
            
            ShowText(textData, callback);
        }

        //Show text data with custom bubble
        public void ShowTextData(string phraseID, Action callback, DialogueCharacterBubbleBase bubble)
        {
            var textData = GetTextData(phraseID);
            bubble.Show(textData, callback);
        }

        private void ShowText(DialogueTextData textData, Action callback)
        {
            if (textData == null)
                callback?.Invoke();
            else
            {
                var textBubble = GetBubble(textData.CharacterID);

                if (textBubble == null)
                {
                    Debug.LogError($"DIALOGUE GRAPH: You are trying to get a character bubble that does not exist. Character ID: '{textData.CharacterID}'.");
                    callback?.Invoke();
                    return;
                }

                if (_currentBubble != null && _currentBubble != textBubble)
                    _currentBubble.Hide();

                _currentBubble = textBubble;
                textBubble.Show(textData, callback);
            }
        }
        
        public DialogueTextData GetTextData(string phraseID)
        {
            if (!_dialogueText.ContainsKey(phraseID)) return null;

            return _dialogueText[phraseID];
        }

        public List<string> GetTextBlock(string blockID)
        {
            var textBlock = new List<string>();

            foreach (var element in _dialogueText)
            {
                var textData = element.Value;
                if (string.IsNullOrEmpty(textData.BlockID)) continue;
                if (textData.BlockID != blockID) continue;
                
                textBlock.Add(textData.PhraseID);
            }

            return textBlock;
        }

        public DialogueCharacterBubbleBase GetBubble(string characterID)
        {
            return _characterBubbles.FirstOrDefault(bubble => bubble.CharacterID == characterID);
        }
        
        #endregion
    }
}
