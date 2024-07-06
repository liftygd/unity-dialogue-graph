using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("String", "Data/String/String", "dialogue-node-data-string")]
    public class DialogueNode_StringData : DialogueGraphDataNode<string>
    {
        [NodeFlow("", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.EmptyPort))]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);

        [NodeFlowField("")] 
        public string StringField;
        
        [NodeFlow("Out", NodeFlowType.FlowOutput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);
        
        public DialogueNode_StringData() : base()
        {
            StringField = "Text";
        }

        public override void Process()
        {
            _data = StringField;
        }

        public override string GetData()
        {
            Process();
            return base.GetData();
        }
    }
}
