using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer + Integer", "Data/Integer/Integer + Integer", "dialogue-node-data-integer")]
    public class DialogueNode_IntegerAdd : DialogueGraphDataNode<int>
    {
        [NodeFlow("Integer 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode IntegerOneIn = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 1")] 
        public int IntegerOneField;
        
        [NodeFlow("Integer 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode IntegerTwoIn = new DialogueGraphNode(true);
        
        [NodeFlowField("Integer 2")] 
        public int IntegerTwoField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private int _addIntOne;
        private int _addIntTwo;
        
        public DialogueNode_IntegerAdd() : base()
        {
            IntegerOneField = 0;
            IntegerTwoField = 0;
        }
        
        public override void Process()
        {
            if (IntegerOneIn != null && IntegerOneIn.ID != "")
                _addIntOne = ((DialogueGraphDataNode<int>) IntegerOneIn).GetData();
            else
                _addIntOne = IntegerOneField;
            
            if (IntegerTwoIn != null && IntegerTwoIn.ID != "")
                _addIntTwo = ((DialogueGraphDataNode<int>) IntegerTwoIn).GetData();
            else
                _addIntTwo = IntegerTwoField;
            
            _data = _addIntOne + _addIntTwo;
        }

        public override int GetData()
        {
            Process();
            return base.GetData();
        }
    }
}
