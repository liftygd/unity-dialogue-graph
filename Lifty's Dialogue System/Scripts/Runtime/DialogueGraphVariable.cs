using UnityEngine;

namespace Lifty.DialogueSystem
{
    [System.Serializable]
    public class DialogueGraphVariable<T>
    {
        public string Name { get; private set; }
        public T Value { get; private set; }

        public DialogueGraphVariable(string variableName, T variableValue)
        {
            Name = variableName;
            Value = variableValue;
        }

        public void SetValue(T value)
        {
            Value = value;
        }
    }
}
