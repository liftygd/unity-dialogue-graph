using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Debug Log", "Process/Debug Log")]
    public class DialogueNode_Log : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Log", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode LogConnection = new DialogueGraphNode(true);
        
        [NodeFlowField("Log")] 
        public string LogString;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private string _logString;

        public DialogueNode_Log() : base()
        {
            LogString = "Debug Log Node";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (LogConnection != null && LogConnection.ID != "")
                _logString = GetDataFromNode<string>(LogConnection, runner);
            else
                _logString = LogString;
            
            Debug.Log(_logString);
            OutConnection.Process(runner);
        }
    }
}
