using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Boolean", "Data/Boolean/Boolean", "dialogue-node-data-boolean")]
    public class DialogueNode_BooleanData : DialogueGraphDataNode<bool>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InBoolean = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public bool FieldBoolean;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutBoolean = new DialogueGraphNode(true);
        
        public DialogueNode_BooleanData() : base()
        {
            FieldBoolean = false;
        }

        public override bool GetData(DialogueGraphRunner runner)
        {
            _data = FieldBoolean;
            return base.GetData(runner);
        }
    }
}
