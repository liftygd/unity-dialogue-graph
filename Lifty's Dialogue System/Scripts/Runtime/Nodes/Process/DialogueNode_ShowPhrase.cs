using System.Collections;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Show Dialogue Phrase", "Process/Show Phrase", "dialogue-node-process")]
    public class DialogueNode_ShowPhrase : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Phrase Id", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InPhraseID = new DialogueGraphNode(true);
        
        [NodeFlowField("Phrase Id")] 
        public string FieldPhraseID;
        private string _phraseID;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private DialogueTextData _dialogueTextData;
        
        public DialogueNode_ShowPhrase() : base()
        {
            FieldPhraseID = "Phrase ID";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InPhraseID != null && InPhraseID.ID != "")
                _phraseID = GetDataFromNode<string>(InPhraseID, runner);
            else
                _phraseID = FieldPhraseID;

            runner.ShowTextData(_phraseID, () => OutConnection.Process(runner));
        }
    }
}
