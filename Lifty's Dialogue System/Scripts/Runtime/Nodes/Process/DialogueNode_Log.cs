using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Debug Log", "Process/Debug Log")]
    public class DialogueNode_Log : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Log", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InLogString = new DialogueGraphNode(true);
        
        [NodeFlowField("Log")] 
        public string FieldLogString;
        private string _logString;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        public DialogueNode_Log() : base()
        {
            FieldLogString = "Debug Log Node";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InLogString != null && InLogString.ID != "")
                _logString = GetDataFromNode<string>(InLogString, runner);
            else
                _logString = FieldLogString;
            
            Debug.Log(_logString);
            OutConnection.Process(runner);
        }
    }
}
