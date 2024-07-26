using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Sequence (2)", "Branching/Sequence/Sequence (2)", "dialogue-node-branch")]
    public class DialogueNode_Sequence2 : DialogueGraphNode
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

        private int _currentExit;

        public DialogueNode_Sequence2()
        {
            FieldRepeat = true;
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);
            
            if (InRepeat != null && InRepeat.ID != "")
                _repeat = GetDataFromNode<bool>(InRepeat, runner);
            else
                _repeat = FieldRepeat;
            
            if (_currentExit == 0)
                OutConnection1.Process(runner);
            else
                OutConnection2.Process(runner);

            _currentExit++;
            if (_currentExit >= 2)
                _currentExit = _repeat ? 0 : 1;
        }
    }
}
