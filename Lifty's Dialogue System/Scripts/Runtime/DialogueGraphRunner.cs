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
        [SerializeField] private DialogueGraphAsset _dialogueGraph;
        [SerializeField] private DialogueFileData _dialogueFile;
        [SerializeField] private bool _runOnStart;

        private bool _dialogueRunning;
        private Dictionary<string, DialogueTextData> _dialogueText;

        [Header("UI")]
        [SerializeField] private List<DialogueCharacterBubbleBase> _characterBubbles;
        private DialogueCharacterBubbleBase _currentBubble;

        [Header("Events")] 
        [SerializeField] private List<DialogueGraphEvent> _events;

        private void Start()
        {
            LoadFile();
            if (_dialogueGraph == null) return;
            
            DialogueGraphController.Instance.DialogueLanguageChanged += LoadFile;
            _dialogueGraph.Nodes.ForEach(node => node.Configurate());

            if (_runOnStart) StartDialogue();
        }

        private void OnDestroy()
        {
            DialogueGraphController.Instance.DialogueLanguageChanged -= LoadFile;
        }

        #region Dialogue

        public void StartDialogue()
        {
            if (_dialogueRunning) return;
            
            _dialogueRunning = true;
            var startNode = GetStartNode();
            startNode.Process(this);
        }

        public DialogueNode_Start GetStartNode()
        {
            return _dialogueGraph.GetStartNode();
        }

        public void EndDialogue()
        {
            HideBubble();

            _currentBubble = null;
            _dialogueRunning = false;
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
        
        private void LoadFile()
        {
            if (_dialogueFile == null) return;
            
            _dialogueText = new Dictionary<string, DialogueTextData>();
            var textFile = _dialogueFile.GetFileByLanguage(DialogueGraphController.Instance.CurrentDialogueLanguage);
            
            DialogueTextData[] textData = JsonHelper.FromJson<DialogueTextData>(textFile.DialogueTextFile.text);
            foreach (var data in textData)
            {
                _dialogueText.Add(data.PhraseID, data);
            }
        }

        public void ShowTextData(string phraseID, Action callback)
        {
            var textData = GetTextData(phraseID);
            ShowText(textData, callback);
        }

        public void ShowTextData(DialogueTextData textData, Action callback)
        {
            ShowText(textData, callback);
        }

        public void ShowTextData(string phraseID, Action callback, DialogueGraphVariable<object>[] variablesToReplace)
        {
            var textData = new DialogueTextData(GetTextData(phraseID));

            foreach (var variable in variablesToReplace)
            {
                textData.Phrase = textData.Phrase.Replace(variable.VariableName, variable.GetValue().ToString());
            }
            
            ShowText(textData, callback);
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
