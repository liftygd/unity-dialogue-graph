using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Sequence (4)", "Branching/Sequence/Sequence (4)", "dialogue-node-branch")]
    public class DialogueNode_Sequence4 : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Repeat Sequence", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InRepeat = new DialogueGraphNode(true);
        
        [NodeFlowField("Repeat Sequence")] 
        public bool FieldRepeat;
        private bool _repeat;

        [NodeFlow("Out 1", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection1 = new DialogueGraphNode(true);

        [NodeFlow("Out 2", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection2 = new DialogueGraphNode(true);
        
        [NodeFlow("Out 3", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection3 = new DialogueGraphNode(true);
        
        [NodeFlow("Out 4", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FlowPort))]
        [SerializeReference] public DialogueGraphNode OutConnection4 = new DialogueGraphNode(true);

        private int _currentExit;
        
        public DialogueNode_Sequence4()
        {
            FieldRepeat = true;
        }
        
        public override void Configurate()
        {
            _currentExit = 0;
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            
            if (_currentExit == 0)
                OutConnection1.Process(runner);
            else if (_currentExit == 1)
                OutConnection2.Process(runner);
            else if (_currentExit == 2)
                OutConnection3.Process(runner);
            else
                OutConnection4.Process(runner);

            _currentExit++;
            if (_currentExit >= 4)
                _currentExit = _repeat ? 0 : 3;
        }
    }
}