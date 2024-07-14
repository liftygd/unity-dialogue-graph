using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Delay", "Process/Delay", "dialogue-node-process")]
    public class DialogueNode_Delay : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlow("Delay Time", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InDelayTime = new DialogueGraphNode(true);
        
        [NodeFlowField("Delay Time")] 
        public float FieldDelayTime;
        private float _delayTime;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_Delay() : base()
        {
            FieldDelayTime = 0f;
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InDelayTime != null && InDelayTime.ID != "")
                _delayTime = GetDataFromNode<float>(InDelayTime, runner);
            else
                _delayTime = FieldDelayTime;

            runner.DelayCallback(_delayTime, () => OutConnection.Process(runner));
        }
    }
}