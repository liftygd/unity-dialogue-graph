using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphController : MonoBehaviour
    {
        public static DialogueGraphController Instance;

        protected void Awake()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            Instance = this;
            _variables = new Dictionary<string, DialogueGraphVariable<object>>();
        }

        #region Variables
        
        protected Dictionary<string, DialogueGraphVariable<object>> _variables;

        public virtual T GetVariableValue<T>(string variableName)
        {
            if (!_variables.ContainsKey(variableName))
                SetVariableValue(variableName, default(T));
            
            var variable = (T) _variables[variableName].Value;
            return variable;
        }

        public virtual void SetVariableValue<T>(string variableName, T value)
        {
            if (!_variables.ContainsKey(variableName))
            {
                var newVariable = new DialogueGraphVariable<object>(variableName, value);
                _variables.Add(variableName, newVariable);
                return;
            }
            
            _variables[variableName].SetValue(value);
        }
        
        #endregion

        #region Localization

        public DialogueLanguageEnum CurrentDialogueLanguage { get; private set; }
        public Action<DialogueLanguageEnum> DialogueLanguageChanged;

        public virtual void ChangeDialogueLanguage(DialogueLanguageEnum newLanguage)
        {
            CurrentDialogueLanguage = newLanguage;
            DialogueLanguageChanged?.Invoke(newLanguage);
        }

        #endregion
    }
}
