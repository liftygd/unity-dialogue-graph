using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("String + String", "Data/String/String + String", "dialogue-node-data-string")]
    public class DialogueNode_StringAppend : DialogueGraphDataNode<string>
    {
        [NodeFlow("String 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InStringOne = new DialogueGraphNode(true);
        
        [NodeFlowField("String 1")] 
        public string FieldStringOne;
        private string _stringOne;
        
        [NodeFlow("String 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InStringTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("String 2")] 
        public string FieldStringTwo;
        private string _stringTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutStringValue = new DialogueGraphNode(true);

        public DialogueNode_StringAppend() : base()
        {
            FieldStringOne = "Text 1";
            FieldStringTwo = "Text 2";
        }

        public override string GetData(DialogueGraphRunner runner)
        {
            if (InStringOne != null && InStringOne.ID != "")
                _stringOne = GetDataFromNode<string>(InStringOne, runner);
            else
                _stringOne = FieldStringOne;
            
            if (InStringTwo != null && InStringTwo.ID != "")
                _stringTwo = GetDataFromNode<string>(InStringTwo, runner);
            else
                _stringTwo = FieldStringTwo;
            
            _data = _stringOne + _stringTwo;

            return base.GetData(runner);
        }
    }
}
