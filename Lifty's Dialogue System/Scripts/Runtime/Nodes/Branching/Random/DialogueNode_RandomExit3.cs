using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Random Exit (3)", "Branching/Random/Random Exit (3)", "dialogue-node-branch")]
    public class DialogueNode_RandomExit3 : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlow("Out 1", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection1 = new DialogueGraphNode(true);

        [NodeFlow("Out 2", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection2 = new DialogueGraphNode(true);
        
        [NodeFlow("Out 3", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection3 = new DialogueGraphNode(true);

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            
            var randVal = Random.Range(0, 3);
            
            if (randVal == 0)
                OutConnection1.Process(runner);
            else if (randVal == 1)
                OutConnection2.Process(runner);
            else
                OutConnection3.Process(runner);
        }
    }
}
