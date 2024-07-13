using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Get Integer Variable", "Data/Integer/Variables/Get Integer Variable", "dialogue-node-data-integer")]
    public class DialogueNode_GetIntegerVariable : DialogueGraphDataNode<int>
    {
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name")] 
        public string FieldVariableName;
        private string _variableName;

        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_GetIntegerVariable() : base()
        {
            FieldVariableName = "Variable Name";
        }
        
        public override int GetData(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            _data = runner.GetVariableValue<int>(_variableName);
            return base.GetData(runner);
        }
    }
}
