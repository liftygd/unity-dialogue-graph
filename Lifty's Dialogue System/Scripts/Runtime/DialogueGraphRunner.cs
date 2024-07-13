using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private void Start()
        {
            LoadFile();

            if (_runOnStart) StartDialogue();
        }

        private void OnEnable()
        {
            DialogueGraphController.Instance.DialogueLanguageChanged += LoadFile;
        }

        private void OnDisable()
        {
            DialogueGraphController.Instance.DialogueLanguageChanged -= LoadFile;
        }

        public void StartDialogue()
        {
            if (_dialogueRunning) return;
            
            var startNode = _dialogueGraph.GetStartNode();
            startNode.Process(this);
            _dialogueRunning = true;
        }

        public void EndDialogue()
        {
            if (_currentBubble != null)
                _currentBubble.Hide();

            _currentBubble = null;
            _dialogueRunning = false;
        }

        #region Variables

        public T GetVariableValue<T>(string variableName)
        {
            return (T) DialogueGraphController.Instance.GetVariableValue<T>(variableName);
        }

        public void SetVariableValue<T>(string variableName, T variableValue)
        {
            DialogueGraphController.Instance.SetVariableValue<T>(variableName, variableValue);
        }

        #endregion

        #region TextData
        
        private void LoadFile()
        {
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

        public DialogueCharacterBubbleBase GetBubble(string characterID)
        {
            return _characterBubbles.FirstOrDefault(bubble => bubble.CharacterID == characterID);
        }
        
        #endregion
    }
}
