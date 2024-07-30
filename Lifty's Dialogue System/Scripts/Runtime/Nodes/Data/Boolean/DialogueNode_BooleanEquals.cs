using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Boolean Equals", "Data/Boolean/Boolean Equals", "dialogue-node-data-boolean")]
    public class DialogueNode_BooleanEquals : DialogueGraphDataNode<bool>
    {
        [NodeFlow("Boolean 1", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InBooleanOne = new DialogueGraphNode(true);
        
        [NodeFlowField("Boolean 1", typeof(Toggle))] 
        public bool FieldBooleanOne;
        private bool _booleanOne;
        
        [NodeFlow("Boolean 2", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InBooleanTwo = new DialogueGraphNode(true);
        
        [NodeFlowField("Boolean 2", typeof(Toggle))] 
        public bool FieldBooleanTwo;
        private bool _booleanTwo;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutBooleanValue = new DialogueGraphNode(true);

        public DialogueNode_BooleanEquals() : base()
        {
            FieldBooleanOne = false;
            FieldBooleanTwo = false;
        }
        
        public override bool GetData(DialogueGraphRunner runner)
        {
            if (InBooleanOne != null && InBooleanOne.ID != "")
                _booleanOne = GetDataFromNode<bool>(InBooleanOne, runner);
            else
                _booleanOne = FieldBooleanOne;
            
            if (InBooleanTwo != null && InBooleanTwo.ID != "")
                _booleanTwo = GetDataFromNode<bool>(InBooleanTwo, runner);
            else
                _booleanTwo = FieldBooleanTwo;
            
            _data = _booleanOne == _booleanTwo;

            return base.GetData(runner);
        }
    }
}
