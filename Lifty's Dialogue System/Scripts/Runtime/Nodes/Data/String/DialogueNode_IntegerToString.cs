using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Integer To String", "Data/String/Integer To String", "dialogue-node-data-string")]
    public class DialogueNode_IntegerToString : DialogueGraphDataNode<string>
    {
        [NodeFlow("In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlowField("In")] 
        public int IntField;
        private int _intFieldValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_IntegerToString() : base()
        {
            IntField = 0;
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            
            if (InConnection != null && InConnection.ID != "")
            {
                var node = ((DialogueGraphDataNode<int>) InConnection);
                node.Process(runner);
                
                _intFieldValue = node.GetData();
            }
            else
                _intFieldValue = IntField;
            
            _data = _intFieldValue.ToString();
        }
    }
}
