using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer + Integer", "Data/Integer/Integer + Integer", "dialogue-node-data-integer")]
    public class DialogueNode_IntegerAdd : DialogueGraphDataNode<int>
    {
        [NodeFlow("Integer 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InIntegerOne = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 1")] 
        public int FieldIntegerOne;
        private int _integerOne;
        
        [NodeFlow("Integer 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InIntegerTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 2")] 
        public int FieldIntegerTwo;
        private int _integerTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutIntegerValue = new DialogueGraphNode(true);

        public DialogueNode_IntegerAdd() : base()
        {
            FieldIntegerOne = 0;
            FieldIntegerTwo = 0;
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InIntegerOne != null && InIntegerOne.ID != "")
                _integerOne = GetDataFromNode<int>(InIntegerOne, runner);
            else
                _integerOne = FieldIntegerOne;
            
            if (InIntegerTwo != null && InIntegerTwo.ID != "")
                _integerOne = GetDataFromNode<int>(InIntegerTwo, runner);
            else
                _integerTwo = FieldIntegerTwo;
            
            _data = _integerOne + _integerTwo;
        }
    }
}
