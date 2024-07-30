using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem.Variables
{
    [NodeInfo("Get Boolean Variable", "Data/Boolean/Variables/Get Boolean Variable", "dialogue-node-data-boolean")]
    public class DialogueNode_GetBooleanVariable : DialogueGraphDataNode<bool>
    {
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name", typeof(TextField))] 
        public string FieldVariableName;
        private string _variableName;

        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_GetBooleanVariable() : base()
        {
            FieldVariableName = "Variable Name";
        }
        
        public override bool GetData(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            _data = runner.GetVariableValue<bool>(_variableName);
            return base.GetData(runner);
        }
    }
}