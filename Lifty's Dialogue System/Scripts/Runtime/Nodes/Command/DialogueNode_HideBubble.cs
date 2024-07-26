using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Hide Character Bubble", "Command/Hide Character Bubble", "dialogue-node-command")]
    public class DialogueNode_HideBubble : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            runner.HideBubble();
            OutConnection.Process(runner);
        }
    }
}
