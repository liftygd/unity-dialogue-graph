using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Start", "Process/Start")]
    public class DialogueNode_Start : DialogueGraphNode
    {
        [NodeFlow("Dialogue Start", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            OutConnection.Process(runner);
        }
    }
}
