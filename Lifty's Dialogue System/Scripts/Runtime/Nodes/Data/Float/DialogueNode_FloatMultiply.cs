using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Float * Float", "Data/Float/Float * Float", "dialogue-node-data-float")]
    public class DialogueNode_FloatMultiply : DialogueGraphDataNode<float>
    {
        [NodeFlow("Float 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InFloatOne = new DialogueGraphNode(true);
        
        [NodeFlowField("Float 1", typeof(FloatField))] 
        public float FieldFloatOne;
        private float _integerOne;
        
        [NodeFlow("Float 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InFloatTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("Float 2", typeof(FloatField))] 
        public float FieldFloatTwo;
        private float _integerTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode OutFloatValue = new DialogueGraphNode(true);

        public DialogueNode_FloatMultiply() : base()
        {
            FieldFloatOne = 0f;
            FieldFloatTwo = 0f;
        }
        
        public override float GetData(DialogueGraphRunner runner)
        {
            if (InFloatOne != null && InFloatOne.ID != "")
                _integerOne = GetDataFromNode<int>(InFloatOne, runner);
            else
                _integerOne = FieldFloatOne;
            
            if (InFloatTwo != null && InFloatTwo.ID != "")
                _integerTwo = GetDataFromNode<int>(InFloatTwo, runner);
            else
                _integerTwo = FieldFloatTwo;
            
            _data = _integerOne * _integerTwo;

            return base.GetData(runner);
        }
    }
}
