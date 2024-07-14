using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Repeat From Start", "Process/Repeat From Start", "dialogue-node-process")]
    public class DialogueNode_RepeatFromStart : DialogueGraphNode
    {
        [NodeFlow("Repeat", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            runner.GetStartNode().Process(runner);
        }
    }
}
