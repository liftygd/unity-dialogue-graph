using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Repeat From Start", "Command/Repeat From Start", "dialogue-node-command")]
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
