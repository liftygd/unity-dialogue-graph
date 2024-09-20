using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue Graph", menuName = "Lifty's Dialogue/New Dialogue Graph")]
    public class DialogueGraphAsset : ScriptableObject
    {
        public List<DialogueGraphNode> Nodes => _nodes;
        [SerializeReference] private List<DialogueGraphNode> _nodes;

        public List<DialogueGraphConnection> ConnectedPorts => _connectedPorts;
        [SerializeReference] private List<DialogueGraphConnection> _connectedPorts;

        [SerializeReference] public Vector3 GraphViewPosition;
        [SerializeReference] public Vector3 GraphViewScale;

        public DialogueGraphAsset()
        {
            _nodes = new List<DialogueGraphNode>();
            _connectedPorts = new List<DialogueGraphConnection>();

            GraphViewScale = new Vector3(1, 1, 1);
        }

        public DialogueNode_Start GetStartNode()
        {
            DialogueNode_Start[] startNodes = Nodes.OfType<DialogueNode_Start>().ToArray();
            if (startNodes.Length == 0)
            {
                Debug.LogError("No start node present in graph!");
                return null;
            }
            
            if (startNodes.Length > 1) Debug.LogWarning("Multiple start nodes in graph. Might break execution.");

            return startNodes[0];
        }

        public DialogueGraphNode GetNodeByID(string guid)
        {
            return Nodes.FirstOrDefault(node => node.ID == guid);
        }

        public void AddConnection(string outPortId, string inPortId)
        {
            _connectedPorts.Add(new DialogueGraphConnection(outPortId, inPortId));
        }

        public void RemoveConnection(string outPortId)
        {
            for (int i = _connectedPorts.Count - 1; i >= 0; i--)
            {
                if (_connectedPorts[i].OutPortID != outPortId) continue;

                _connectedPorts.Remove(_connectedPorts[i]);
            }
        }
    }
}
