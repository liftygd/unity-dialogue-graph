using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Boolean", "Data/Boolean/Boolean", "dialogue-node-data-boolean")]
    public class DialogueNode_BooleanData : DialogueGraphDataNode<bool>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public bool BooleanField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_BooleanData() : base()
        {
            BooleanField = false;
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            
            _data = BooleanField;
        }
    }
}
