using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer Comparison", "Data/Integer/Integer Comparison", "dialogue-node-data-integer")]
    public class DialogueNode_IntegerComparison : DialogueGraphDataNode<bool>
    {
        [NodeFlow("Integer 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InIntegerOne = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 1", typeof(IntegerField))] 
        public int FieldIntegerOne;
        private int _integerOne;
        
        [NodeFlow("Comparison", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InComparison = new DialogueGraphNode(true);
        
        [NodeFlowField("Comparison", typeof(EnumField))] 
        public DialogueNode_ComparisonEnum FieldComparison;

        [NodeFlow("Integer 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InIntegerTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 2", typeof(IntegerField))] 
        public int FieldIntegerTwo;
        private int _integerTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutBooleanValue = new DialogueGraphNode(true);

        public DialogueNode_IntegerComparison() : base()
        {
            FieldIntegerOne = 0;
            FieldIntegerTwo = 0;
            FieldComparison = DialogueNode_ComparisonEnum.Equals;
        }
        
        public override bool GetData(DialogueGraphRunner runner)
        {
            if (InIntegerOne != null && InIntegerOne.ID != "")
                _integerOne = GetDataFromNode<int>(InIntegerOne, runner);
            else
                _integerOne = FieldIntegerOne;
            
            if (InIntegerTwo != null && InIntegerTwo.ID != "")
                _integerTwo = GetDataFromNode<int>(InIntegerTwo, runner);
            else
                _integerTwo = FieldIntegerTwo;

            switch (FieldComparison)
            {
                case DialogueNode_ComparisonEnum.Less:
                    _data = _integerOne < _integerTwo;
                    break;
                case DialogueNode_ComparisonEnum.LessOrEquals:
                    _data = _integerOne <= _integerTwo;
                    break;
                case DialogueNode_ComparisonEnum.Equals:
                    _data = _integerOne == _integerTwo;
                    break;
                case DialogueNode_ComparisonEnum.MoreOrEquals:
                    _data = _integerOne >= _integerTwo;
                    break;
                case DialogueNode_ComparisonEnum.More:
                    _data = _integerOne > _integerTwo;
                    break;
            }

            return base.GetData(runner);
        }
    }
}