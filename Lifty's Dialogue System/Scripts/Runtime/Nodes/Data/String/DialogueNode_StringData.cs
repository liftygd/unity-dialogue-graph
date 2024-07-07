using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("String", "Data/String/String", "dialogue-node-data-string")]
    public class DialogueNode_StringData : DialogueGraphDataNode<string>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InString = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public string FieldString;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutString = new DialogueGraphNode(true);
        
        public DialogueNode_StringData() : base()
        {
            FieldString = "Text";
        }

        public override string GetData(DialogueGraphRunner runner)
        {
            _data = FieldString;
            return base.GetData(runner);
        }
    }
}
