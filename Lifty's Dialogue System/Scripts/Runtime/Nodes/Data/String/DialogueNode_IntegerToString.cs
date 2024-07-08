using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer To String", "Data/String/Integer To String", "dialogue-node-data-string")]
    public class DialogueNode_IntegerToString : DialogueGraphDataNode<string>
    {
        [NodeFlow("In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InIntValue = new DialogueGraphNode(true);

        [NodeFlowField("In")] 
        public int FieldIntValue;
        private int _intValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutStringValue = new DialogueGraphNode(true);
        
        public DialogueNode_IntegerToString() : base()
        {
            FieldIntValue = 0;
        }

        public override string GetData(DialogueGraphRunner runner)
        {
            if (InIntValue != null && InIntValue.ID != "")
                _intValue = GetDataFromNode<int>(InIntValue, runner);
            else
                _intValue = FieldIntValue;
            
            _data = _intValue.ToString();

            return base.GetData(runner);
        }
    }
}
