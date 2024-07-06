using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [Serializable]
    public class DialogueGraphConnection
    {
        public DialogueGraphNode Node;
        public string ConnectedPortName;

        public DialogueGraphConnection(DialogueGraphNode node, string connectedPortName)
        {
            Node = node;
            ConnectedPortName = connectedPortName;
        }
    }
}
