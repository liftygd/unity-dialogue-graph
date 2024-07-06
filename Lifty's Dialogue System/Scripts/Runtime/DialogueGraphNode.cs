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

        public DialogueGraphNode(bool empty = false)
        {
            if (empty) return;
            
            NewGUID();
        }

        private void NewGUID()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public virtual void Configure()
        {
            
        }

        public void SetPosition(Rect position)
        {
            _position = position;
        }

        public virtual void Process()
        {
            
        }
    }
}
