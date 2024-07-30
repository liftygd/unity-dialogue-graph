using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Set String Variable", "Data/String/Variables/Set String Variable", "dialogue-node-data-string")]
    public class DialogueNode_SetStringVariable : DialogueGraphDataNode<string>
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name", typeof(TextField))] 
        public string FieldVariableName;
        private string _variableName;
        
        [NodeFlow("Value In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableValue = new DialogueGraphNode(true);
        
        [NodeFlowField("Value In", typeof(TextField))] 
        public string FieldVariableValue;
        private string _variableValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_SetStringVariable() : base()
        {
            FieldVariableName = "Variable Name";
            FieldVariableValue = "Variable Value";
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            if (InVariableValue != null && InVariableValue.ID != "")
                _variableValue = GetDataFromNode<string>(InVariableValue, runner);
            else
                _variableValue = FieldVariableValue;
            
            runner.SetVariableValue<string>(_variableName, _variableValue);
            _data = _variableValue;
            
            OutConnection.Process(runner);
        }
    }
}
