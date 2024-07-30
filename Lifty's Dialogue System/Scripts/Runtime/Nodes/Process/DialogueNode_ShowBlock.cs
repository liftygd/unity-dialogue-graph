using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Show Dialogue Block", "Process/Show Block", "dialogue-node-process")]
    public class DialogueNode_ShowBlock : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Phrase Id", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InBlockID = new DialogueGraphNode(true);
        
        [NodeFlowField("Phrase Id", typeof(TextField))] 
        public string FieldBlockID;
        private string _blockID;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private int _currentPhrase;
        private List<string> _textBlock;

        public DialogueNode_ShowBlock() : base()
        {
            FieldBlockID = "Block ID";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InBlockID != null && InBlockID.ID != "")
                _blockID = GetDataFromNode<string>(InBlockID, runner);
            else
                _blockID = FieldBlockID;

            _currentPhrase = 0;
            _textBlock = runner.GetTextBlock(_blockID);
            ShowNextPhrase();
        }

        private void ShowNextPhrase()
        {
            if (_currentPhrase >= _textBlock.Count)
            {
                OutConnection.Process(_runner);
                return;
            }
            
            _runner.ShowTextData(_textBlock[_currentPhrase], ShowNextPhrase);
            _currentPhrase++;
        }
    }
}
