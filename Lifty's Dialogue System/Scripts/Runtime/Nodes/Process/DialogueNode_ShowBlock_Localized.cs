using System.Collections.Generic;
using UnityEngine;
#if LOCALIZATION_159
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
#endif
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    [NodeInfo("Show Dialogue Block Localized", "Process/Show Block Localized", "dialogue-node-process")]
    public class DialogueNode_ShowBlock_Localized : DialogueGraphNode
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
        
#if LOCALIZATION_159
        private StringTable _table;
#endif
        
        [NodeFlow("Character Id", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.StringPort))]
        [SerializeReference] public DialogueGraphNode InCharacterID = new DialogueGraphNode(true);
        
        [NodeFlowField("Character Id", typeof(TextField))] 
        public string CharacterID;
        private string _characterID;
        
        [NodeFlow("Delay Time", NodeFlowType.FlowInput, typeof(DialogueGraphPortTypes.FloatPort))]
        [SerializeReference] public DialogueGraphNode InDelayTime = new DialogueGraphNode(true);
        
        [NodeFlowField("Delay Time", typeof(FloatField))] 
        public float FieldDelayTime;
        private float _delayTime;

        [NodeFlow("Out", NodeFlowType.FlowOutput)]
        [SerializeReference] public DialogueGraphNode OutConnection = new DialogueGraphNode(true);

        private int _currentPhrase;
        private List<string> _textBlock;

        public DialogueNode_ShowBlock_Localized() : base()
        {
            FieldBlockPrefix = "BlockPrefix";
            TableName = "TableName";
            CharacterID = "CharacterID";
            FieldDelayTime = 1.3f;
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
            
            if (InCharacterID != null && InCharacterID.ID != "")
                _characterID = GetDataFromNode<string>(InCharacterID, runner);
            else
                _characterID = CharacterID;
            
            if (InDelayTime != null && InDelayTime.ID != "")
                _delayTime = GetDataFromNode<float>(InDelayTime, runner);
            else
                _delayTime = FieldDelayTime;

            _currentPhrase = 0;
#if LOCALIZATION_159
            _table = LocalizationSettings.StringDatabase.GetTable(_tableName);
            if (_table == null)
                OutConnection.Process(_runner);
            else
                ShowNextPhrase();
#else
            OutConnection.Process(_runner);
#endif
        }

        private void ShowNextPhrase()
        {
#if LOCALIZATION_159
            var text = _table.GetEntry($"{_blockPrefix}_{_currentPhrase+1}");
            
            if (text == null ||  string.IsNullOrEmpty(text.GetLocalizedString()))
            {
                OutConnection.Process(_runner);
                return;
            }
            
            _runner.ShowTextData(new DialogueTextData { Phrase = text.GetLocalizedString(), CharacterID = _characterID, PhraseTime = _delayTime }, ShowNextPhrase);
            _currentPhrase++;
#endif
        }
    }
}