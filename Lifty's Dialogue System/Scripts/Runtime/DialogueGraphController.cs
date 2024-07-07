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

        private Dictionary<string, object> _variables;

        public object GetVariableValue<T>(string variableName)
        {
            if (!_variables.ContainsKey(variableName)) return null;
            
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
    }
}
