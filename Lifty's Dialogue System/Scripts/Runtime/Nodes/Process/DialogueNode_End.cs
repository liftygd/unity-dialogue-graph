using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Dialogue End", "Process/Dialogue End", "dialogue-node-process")]
    public class DialogueNode_End : DialogueGraphNode
    {
        [NodeFlow("End", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            runner.EndDialogue();
        }
    }
}
