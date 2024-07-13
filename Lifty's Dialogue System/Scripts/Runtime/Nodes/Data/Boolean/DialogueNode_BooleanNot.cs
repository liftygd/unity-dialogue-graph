using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Boolean Not", "Data/Boolean/Boolean Not", "dialogue-node-data-boolean")]
    public class DialogueNode_BooleanNot : DialogueGraphDataNode<bool>
    {
        [NodeFlow("Boolean", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InBoolean = new DialogueGraphNode(true);
        
        [NodeFlowField("Boolean")] 
        public bool FieldBooleanOne;
        private bool _booleanOne;

        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutBooleanValue = new DialogueGraphNode(true);

        public DialogueNode_BooleanNot() : base()
        {
            FieldBooleanOne = false;
        }
        
        public override bool GetData(DialogueGraphRunner runner)
        {
            if (InBoolean != null && InBoolean.ID != "")
                _booleanOne = GetDataFromNode<bool>(InBoolean, runner);
            else
                _booleanOne = FieldBooleanOne;

            _data = !_booleanOne;

            return base.GetData(runner);
        }
    }
}