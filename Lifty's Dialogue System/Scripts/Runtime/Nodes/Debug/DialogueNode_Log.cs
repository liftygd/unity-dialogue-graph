using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Debug Log", "Debug/Debug Log", "dialogue-node-debug")]
    public class DialogueNode_Log : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("LogType", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InLogType = new DialogueGraphNode(true);

        [NodeFlowField("LogType", typeof(EnumField))] 
        public DialogueLogType FieldLogType;

        [NodeFlow("Log Text", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InLogString = new DialogueGraphNode(true);
        
        [NodeFlowField("Log Text", typeof(TextField))] 
        public string FieldLogString;
        private string _logString;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        public DialogueNode_Log() : base()
        {
            FieldLogString = "Debug Log Node";
            FieldLogType = DialogueLogType.Log;
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InLogString != null && InLogString.ID != "")
                _logString = GetDataFromNode<string>(InLogString, runner);
            else
                _logString = FieldLogString;

            switch (FieldLogType)
            {
                case DialogueLogType.Log:
                    Debug.Log(_logString);
                    break;
                case DialogueLogType.Error:
                    Debug.LogError(_logString);
                    break;;
                case DialogueLogType.Warning:
                    Debug.LogWarning(_logString);
                    break;;
            }

            OutConnection.Process(runner);
        }
    }

    public enum DialogueLogType
    {
        Log,
        Error,
        Warning
    }
}
