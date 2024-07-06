using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("String + String", "Data/String/String + String", "dialogue-node-data-string")]
    public class DialogueNode_StringAppend : DialogueGraphDataNode<string>
    {
        [NodeFlow("String 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode StringOneIn = new DialogueGraphNode(true);
        
        [NodeFlowField("String 1")] 
        public string StringOneField;
        
        [NodeFlow("String 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode StringTwoIn = new DialogueGraphNode(true);
        
        [NodeFlowField("String 2")] 
        public string StringTwoField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        private string _addStringOne;
        private string _addStringTwo;
        
        public DialogueNode_StringAppend() : base()
        {
            StringOneField = "Text 1";
            StringTwoField = "Text 2";
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (StringOneIn != null && StringOneIn.ID != "")
                _addStringOne = GetDataFromNode<string>(StringOneIn, runner);
            else
                _addStringOne = StringOneField;
            
            if (StringTwoIn != null && StringTwoIn.ID != "")
                _addStringTwo = GetDataFromNode<string>(StringTwoIn, runner);
            else
                _addStringTwo = StringTwoField;
            
            _data = _addStringOne + _addStringTwo;
        }
    }
}
