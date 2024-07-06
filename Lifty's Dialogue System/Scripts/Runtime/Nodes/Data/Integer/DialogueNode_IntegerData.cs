using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer", "Data/Integer/Integer", "dialogue-node-data-integer")]
    public class DialogueNode_IntegerData : DialogueGraphDataNode<int>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public int IntegerField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_IntegerData() : base()
        {
            IntegerField = 0;
        }

        public override void Process()
        {
            _data = IntegerField;
        }

        public override int GetData()
        {
            Process();
            return base.GetData();
        }
    }
}
