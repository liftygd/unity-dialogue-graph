using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphVariable<T>
    {
        private T _variable;
        
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
