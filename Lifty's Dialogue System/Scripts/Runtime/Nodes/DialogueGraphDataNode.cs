using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphDataNode<T> : DialogueGraphNode
    {
        protected T _data;

        public virtual T GetData(DialogueGraphRunner runner)
        {
            return _data;
        }
    }
}
