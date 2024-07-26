using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphController : MonoBehaviour
    {
        public static DialogueGraphController Instance;

        private void Awake()
        {
            Instance = this;
            _variables = new Dictionary<string, object>();
        }

        #region Variables
        
        private Dictionary<string, object> _variables;

        public T GetVariableValue<T>(string variableName)
        {
            if (!_variables.ContainsKey(variableName))
                SetVariableValue(variableName, default(T));
            
            var variable = (DialogueGraphVariable<T>) _variables[variableName];
            return variable.GetValue();
        }

        public void SetVariableValue<T>(string variableName, T value)
        {
            if (!_variables.ContainsKey(variableName))
            {
                var newVariable = new DialogueGraphVariable<T>();
                newVariable.SetValue(value);
                
                _variables.Add(variableName, newVariable);
                return;
            }
            
            var currentVariable = (DialogueGraphVariable<T>) _variables[variableName];
            currentVariable.SetValue(value);

            _variables[variableName] = currentVariable;
        }
        
        #endregion

        #region Localization

        public DialogueLanguageEnum CurrentDialogueLanguage { get; private set; }
        public Action DialogueLanguageChanged;

        public void ChangeDialogueLanguage(DialogueLanguageEnum newLanguage)
        {
            CurrentDialogueLanguage = newLanguage;
            DialogueLanguageChanged?.Invoke();
        }

        #endregion
    }
}
