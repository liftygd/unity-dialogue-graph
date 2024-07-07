using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [Serializable]
    public class DialogueGraphConnection
    {
        public string OutPortID => _outPortID;
        [SerializeField] private string _outPortID;

        public string InPortID => _inPortID;
        [SerializeField] private string _inPortID;

        public DialogueGraphConnection(string outPortID, string inPortID)
        {
            _outPortID = outPortID;
            _inPortID = inPortID;
        }
    }
}
