using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Dialogue Start", "Process/Dialogue Start", "dialogue-node-process")]
    public class DialogueNode_Start : DialogueGraphNode
    {
        [NodeFlow("Start", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            OutConnection.Process(runner);
        }
    }
}
