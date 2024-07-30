using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Set Integer Variable", "Data/Integer/Variables/Set Integer Variable", "dialogue-node-data-integer")]
    public class DialogueNode_SetIntegerVariable : DialogueGraphDataNode<int>
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name", typeof(TextField))] 
        public string FieldVariableName;
        private string _variableName;
        
        [NodeFlow("Value In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode InVariableValue = new DialogueGraphNode(true);
        
        [NodeFlowField("Value In", typeof(IntegerField))] 
        public int FieldVariableValue;
        private int _variableValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.IntegerPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_SetIntegerVariable() : base()
        {
            FieldVariableName = "Variable Name";
            FieldVariableValue = 0;
        }
        
        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;

            if (InVariableValue != null && InVariableValue.ID != "")
                _variableValue = GetDataFromNode<int>(InVariableValue, runner);
            else
                _variableValue = FieldVariableValue;

            runner.SetVariableValue<int>(_variableName, _variableValue);
            _data = _variableValue;
            
            OutConnection.Process(runner);
        }
    }
}
