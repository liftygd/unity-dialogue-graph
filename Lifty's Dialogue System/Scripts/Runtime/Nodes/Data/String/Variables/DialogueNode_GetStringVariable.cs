using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Get String Variable", "Data/String/Variables/Get String Variable", "dialogue-node-data-string")]
    public class DialogueNode_GetStringVariable : DialogueGraphDataNode<string>
    {
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name", typeof(TextField))] 
        public string FieldVariableName;
        private string _variableName;

        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_GetStringVariable() : base()
        {
            FieldVariableName = "Variable Name";
        }
        
        public override string GetData(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            _data = runner.GetVariableValue<string>(_variableName);
            return base.GetData(runner);
        }
    }
}
