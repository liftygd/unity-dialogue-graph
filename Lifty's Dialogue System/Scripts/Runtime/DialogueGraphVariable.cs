using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphVariable<T>
    {
        public string VariableName { get; private set; }
        private T _variable;
        
        public DialogueGraphVariable(string variableName)
        {
            VariableName = variableName;
        }
        
        public DialogueGraphVariable(string variableName, T variableValue)
        {
            VariableName = variableName;
            SetValue(variableValue);
        }
        
        public T GetValue()
        {
            return _variable;
        }

        public void SetValue(T value)
        {
            _variable = value;
        }
    }
}
