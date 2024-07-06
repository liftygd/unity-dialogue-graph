using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialogue Graph", menuName = "Lifty's Dialogue/New Dialogue Graph")]
    public class DialogueGraphAsset : SerializedScriptableObject
    {
        [SerializeReference] private List<DialogueGraphNode> _nodes;
        public List<DialogueGraphNode> Nodes => _nodes;
        
        [OdinSerialize] private Dictionary<string, string> _connectedPorts;
        public Dictionary<string, string> ConnectedPorts => _connectedPorts;

        public DialogueGraphAsset()
        {
            _nodes = new List<DialogueGraphNode>();
            _connectedPorts = new Dictionary<string, string>();
        }

        [ContextMenu("Reset Graph")]
        public void Reset()
        {
            _nodes = new List<DialogueGraphNode>();
            _connectedPorts = new Dictionary<string, string>();
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
    }
}
