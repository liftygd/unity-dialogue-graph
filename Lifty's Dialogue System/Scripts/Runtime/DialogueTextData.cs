using UnityEngine;

namespace Lifty.DialogueSystem
{
    [System.Serializable]
    public class DialogueTextData
    {
        public string PhraseID;
        public string Phrase;
        public string CharacterID;
        public string CharacterName;
        public int PhraseTime;

        public string BlockID;

        public DialogueTextData(string phraseID = "", string phrase = "", string characterID = "", string characterName = "", int phraseTime = 1)
        {
            PhraseID = phraseID;
            Phrase = phrase;
            CharacterID = characterID;
            CharacterName = characterName;
            PhraseTime = phraseTime;
            BlockID = "";
        }
    }
}
