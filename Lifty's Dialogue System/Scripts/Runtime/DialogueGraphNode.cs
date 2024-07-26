using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [System.Serializable]
    public class DialogueGraphNode
    {
        [SerializeField] private string _guid;
        [SerializeField] private Rect _position;

        public string TypeName;
        public string ID => _guid;
        public Rect Position => _position;

        protected DialogueGraphRunner _runner;

        public DialogueGraphNode(bool empty = false)
        {
            if (empty) return;
            
            NewGUID();
        }

        private void NewGUID()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public void SetPosition(Rect position)
        {
            _position = position;
        }

        public virtual void Configurate()
        {
            
        }

        public virtual void Process(DialogueGraphRunner runner)
        {
            _runner = runner;
        }

        protected T GetDataFromNode<T>(DialogueGraphNode node, DialogueGraphRunner runner)
        {
            var dataNode = (DialogueGraphDataNode<T>) node;
            return dataNode.GetData(runner);
        }
    }
}
