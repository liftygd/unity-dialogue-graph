using UnityEngine;

namespace Lifty.DialogueSystem.Variables
{
    [NodeInfo("Set Boolean Variable", "Data/Boolean/Variables/Set Boolean Variable", "dialogue-node-data-boolean")]
    public class DialogueNode_SetBooleanVariable : DialogueGraphDataNode<bool>
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name")] 
        public string FieldVariableName;
        private string _variableName;
        
        [NodeFlow("Value In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode InVariableValue = new DialogueGraphNode(true);
        
        [NodeFlowField("Value In")] 
        public bool FieldVariableValue;
        private bool _variableValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.BooleanPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_SetBooleanVariable() : base()
        {
            FieldVariableName = "Variable Name";
            FieldVariableValue = false;
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            if (InVariableValue != null && InVariableValue.ID != "")
                _variableValue = GetDataFromNode<bool>(InVariableValue, runner);
            else
                _variableValue = FieldVariableValue;
            
            runner.SetVariableValue<bool>(_variableName, _variableValue);
            _data = _variableValue;
            
            OutConnection.Process(runner);
        }
    }
}