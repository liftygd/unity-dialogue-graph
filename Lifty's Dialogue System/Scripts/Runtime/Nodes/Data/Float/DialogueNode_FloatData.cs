using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Float", "Data/Float/Float", "dialogue-node-data-float")]
    public class DialogueNode_FloatData : DialogueGraphDataNode<float>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InFloat = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public float FloatField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode OutFloat = new DialogueGraphNode(true);
        
        public DialogueNode_FloatData() : base()
        {
            FloatField = 0;
        }

        public override float GetData(DialogueGraphRunner runner)
        {
            _data = FloatField;
            return base.GetData(runner);
        }
    }
}
