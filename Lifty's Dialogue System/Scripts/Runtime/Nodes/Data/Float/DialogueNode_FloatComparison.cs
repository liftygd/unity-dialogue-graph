using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Float Comparison", "Data/Float/Float Comparison", "dialogue-node-data-float")]
    public class DialogueNode_FloatComparison : DialogueGraphDataNode<bool>
    {
        [NodeFlow("Float 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InFloatOne = new DialogueGraphNode(true);
        
        [NodeFlowField("Float 1")] 
        public float FieldFloatOne;
        private float _floatOne;
        
        [NodeFlow("Comparison", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InComparison = new DialogueGraphNode(true);
        
        [NodeFlowField("Comparison")] 
        public DialogueNode_ComparisonEnum FieldComparison;

        [NodeFlow("Float 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InFloatTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("Float 2")] 
        public float FieldFloatTwo;
        private float _floatTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutBooleanValue = new DialogueGraphNode(true);

        public DialogueNode_FloatComparison() : base()
        {
            FieldFloatOne = 0f;
            FieldFloatTwo = 0f;
            FieldComparison = DialogueNode_ComparisonEnum.Equals;
        }
        
        public override bool GetData(DialogueGraphRunner runner)
        {
            if (InFloatOne != null && InFloatOne.ID != "")
                _floatOne = GetDataFromNode<float>(InFloatOne, runner);
            else
                _floatOne = FieldFloatOne;
            
            if (InFloatTwo != null && InFloatTwo.ID != "")
                _floatTwo = GetDataFromNode<float>(InFloatTwo, runner);
            else
                _floatTwo = FieldFloatTwo;

            switch (FieldComparison)
            {
                case DialogueNode_ComparisonEnum.Less:
                    _data = _floatOne < _floatTwo;
                    break;
                case DialogueNode_ComparisonEnum.LessOrEquals:
                    _data = _floatOne <= _floatTwo;
                    break;
                case DialogueNode_ComparisonEnum.Equals:
                    _data = _floatOne == _floatTwo;
                    break;
                case DialogueNode_ComparisonEnum.MoreOrEquals:
                    _data = _floatOne >= _floatTwo;
                    break;
                case DialogueNode_ComparisonEnum.More:
                    _data = _floatOne > _floatTwo;
                    break;
            }

            return base.GetData(runner);
        }
    }
}