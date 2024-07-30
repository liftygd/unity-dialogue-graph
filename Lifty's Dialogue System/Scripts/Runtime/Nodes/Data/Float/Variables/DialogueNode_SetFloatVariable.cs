using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Set Float Variable", "Data/Float/Variables/Set Float Variable", "dialogue-node-data-float")]
    public class DialogueNode_SetFloatVariable : DialogueGraphDataNode<float>
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name", typeof(TextField))] 
        public string FieldVariableName;
        private string _variableName;
        
        [NodeFlow("Value In", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InVariableValue = new DialogueGraphNode(true);
        
        [NodeFlowField("Value In", typeof(FloatField))] 
        public float FieldVariableValue;
        private float _variableValue;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_SetFloatVariable() : base()
        {
            FieldVariableName = "Variable Name";
            FieldVariableValue = 0f;
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

            runner.SetVariableValue<float>(_variableName, _variableValue);
            _data = _variableValue;
            
            OutConnection.Process(runner);
        }
    }
}
