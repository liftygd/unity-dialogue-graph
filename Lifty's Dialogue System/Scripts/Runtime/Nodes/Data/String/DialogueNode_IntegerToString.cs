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

        public override void Process()
        {
            if (InConnection != null && InConnection.ID != "")
                _intFieldValue = ((DialogueGraphDataNode<int>) InConnection).GetData();
            else
                _intFieldValue = IntField;
            
            _data = _intFieldValue.ToString();
        }

        public override string GetData()
        {
            Process();
            return base.GetData();
        }
    }
}
