using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Call Event", "Command/Call Event", "dialogue-node-command")]
    public class DialogueNode_CallEvent : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Event Id", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InEventID = new DialogueGraphNode(true);
        
        [NodeFlowField("Event Id", typeof(TextField))] 
        public string FieldEventID;
        private string _eventID;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_CallEvent() : base()
        {
            FieldEventID = "Event ID";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InEventID != null && InEventID.ID != "")
                _eventID = GetDataFromNode<string>(InEventID, runner);
            else
                _eventID = FieldEventID;

            runner.CallEvent(_eventID);
            OutConnection.Process(runner);
        }
    }
}
