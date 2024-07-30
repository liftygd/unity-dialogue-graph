using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Condition", "Branching/Condition", "dialogue-node-branch")]
    public class DialogueNode_Condition : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Condition", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InBool = new DialogueGraphNode(true);
        
        [NodeFlowField("Condition", typeof(Toggle))] 
        public bool ConditionField;
        private bool _condition;

        [NodeFlow("True", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnectionTrue = new DialogueGraphNode(true);

        [NodeFlow("False", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnectionFalse = new DialogueGraphNode(true);
        
        public DialogueNode_Condition() : base()
        {
            ConditionField = false;
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InBool != null && InBool.ID != "")
                _condition = GetDataFromNode<bool>(InBool, runner);
            else
                _condition = ConditionField;
            
            if (_condition)
                OutConnectionTrue.Process(runner);
            else
                OutConnectionFalse.Process(runner);
        }
    }
}
