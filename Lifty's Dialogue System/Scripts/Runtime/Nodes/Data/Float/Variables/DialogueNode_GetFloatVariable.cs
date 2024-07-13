using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Get Float Variable", "Data/Float/Variables/Get Float Variable", "dialogue-node-data-float")]
    public class DialogueNode_GetFloatVariable : DialogueGraphDataNode<float>
    {
        [NodeFlow("Variable Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InVariableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Variable Name")] 
        public string FieldVariableName;
        private string _variableName;

        [NodeFlow("Value Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode OutVariableValue = new DialogueGraphNode(true);
        
        public DialogueNode_GetFloatVariable() : base()
        {
            FieldVariableName = "Variable Name";
        }
        
        public override float GetData(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InVariableName != null && InVariableName.ID != "")
                _variableName = GetDataFromNode<string>(InVariableName, runner);
            else
                _variableName = FieldVariableName;
            
            _data = runner.GetVariableValue<float>(_variableName);
            return base.GetData(runner);
        }
    }
}
