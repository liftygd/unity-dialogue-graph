using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Show Dialogue Block Localized", "Process/Show Block Localized", "dialogue-node-process")]
    public class DialogueNode_ShowPhrase_Localized : DialogueGraphNode
    {
        [NodeFlow("In", NodeFlowType.FlowInput)]
        [SerializeReference] public DialogueGraphNode InConnection = new DialogueGraphNode(true);
        
        [NodeFlow("Phrase Id", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InBlockID = new DialogueGraphNode(true);
        
        [NodeFlowField("Phrase Id", typeof(TextField))] 
        public string FieldBlockPrefix;
        private string _blockPrefix;
        
        [NodeFlow("Table Name", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InTableName = new DialogueGraphNode(true);
        
        [NodeFlowField("Table Name", typeof(TextField))] 
        public string TableName;
        private string _tableName;
        private StringTable _table;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private int _currentPhrase;
        private List<string> _textBlock;

        public DialogueNode_ShowPhrase_Localized() : base()
        {
            FieldBlockPrefix = "BlockPrefix";
            TableName = "TableName";
        }

        public override void Process(DialogueGraphRunner runner)
        {
            base.Process(runner);

            if (InBlockID != null && InBlockID.ID != "")
                _blockPrefix = GetDataFromNode<string>(InBlockID, runner);
            else
                _blockPrefix = FieldBlockPrefix;
            
            if (InTableName != null && InTableName.ID != "")
                _tableName = GetDataFromNode<string>(InTableName, runner);
            else
                _tableName = TableName;

            _currentPhrase = 0;
            _table = LocalizationSettings.StringDatabase.GetTable(_tableName);
            if (_table == null)
                OutConnection.Process(_runner);
            else
                ShowPhrase();
        }

        private void ShowPhrase()
        {
            var text = _table.GetEntry($"{_blockPrefix}");
            
            if (text == null ||  string.IsNullOrEmpty(text.GetLocalizedString()))
            {
                OutConnection.Process(_runner);
                return;
            }
            
            _runner.ShowTextData(new DialogueTextData { Phrase = text.GetLocalizedString(), CharacterID = "Player", PhraseTime = 4 }, 
                () => OutConnection.Process(_runner));
        }
    }
}
