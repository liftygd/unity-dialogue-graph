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

        private Dictionary<string, DialogueTextData> _dialogueText;

        [Header("UI")] 
        [SerializeField] private float _printerDelay;
        [SerializeField] private List<DialogueCharacterBubble> _characterBubbles;

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
            var startNode = _dialogueGraph.GetStartNode();
            startNode.Process(this);
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
                StartCoroutine(PrintTextData(textData, callback));
        }

        private IEnumerator PrintTextData(DialogueTextData textData, Action callback)
        {
            var currentCharacter = 0;
            var textLength = textData.Phrase.Length;
            var textUI = GetBubble(textData.CharacterID);

            if (textUI == null)
            {
                Debug.LogError($"DIALOGUE GRAPH: You are trying to get a character bubble that does not exist. Character ID: '{textData.CharacterID}'.");
                callback?.Invoke();
                yield break;
            }

            var phraseText = textData.Phrase;
            textUI.TextUI.text = "";
            
            while (currentCharacter < textLength)
            {
                currentCharacter++;
                textUI.TextUI.text = phraseText.Substring(0, currentCharacter);

                yield return new WaitForSeconds(_printerDelay);
            }
            
            yield return new WaitForSeconds(textData.PhraseTime);

            textUI.TextUI.text = "";
            callback?.Invoke();
        }

        public DialogueTextData GetTextData(string phraseID)
        {
            if (!_dialogueText.ContainsKey(phraseID)) return null;

            return _dialogueText[phraseID];
        }

        public DialogueCharacterBubble GetBubble(string characterID)
        {
            return _characterBubbles.FirstOrDefault(bubble => bubble.CharacterID == characterID);
        }
        
        #endregion
    }
}
