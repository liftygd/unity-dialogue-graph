using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [Serializable]
    public class DialogueCharacterBubbleBase : MonoBehaviour
    {
        public string CharacterID;
        [SerializeField] protected TMP_Text textUI;
        [SerializeField] protected float _printerDelay;

        private void Start()
        {
            textUI.text = "";
        }

        public virtual void Show(DialogueTextData textData, Action callback)
        {
            StartCoroutine(PrintTextData(textData, callback));
        }
        
        protected virtual IEnumerator PrintTextData(DialogueTextData textData, Action callback)
        {
            var currentCharacter = 0;
            var textLength = textData.Phrase.Length;
            var phraseText = textData.Phrase;
            textUI.text = "";
            
            while (currentCharacter < textLength)
            {
                currentCharacter++;
                textUI.text = phraseText.Substring(0, currentCharacter);

                yield return new WaitForSeconds(_printerDelay);
            }
            
            yield return new WaitForSeconds(textData.PhraseTime);
            callback?.Invoke();
        }

        public virtual void Hide()
        {
            textUI.text = "";
        }
    }
}
