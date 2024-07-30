using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer", "Data/Integer/Integer", "dialogue-node-data-integer")]
    public class DialogueNode_IntegerData : DialogueGraphDataNode<int>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InInteger = new DialogueGraphNode(true);

        [NodeFlowField("", typeof(IntegerField))] 
        public int IntegerField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutInteger = new DialogueGraphNode(true);
        
        public DialogueNode_IntegerData() : base()
        {
            IntegerField = 0;
        }

        public override int GetData(DialogueGraphRunner runner)
        {
            _data = IntegerField;
            return base.GetData(runner);
        }
    }
}
