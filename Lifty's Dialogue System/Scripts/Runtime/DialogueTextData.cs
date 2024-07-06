using UnityEngine;

namespace Lifty.DialogueSystem
{
    [System.Serializable]
    public class DialogueTextData
    {
        public string PhraseID;
        public string CharacterName;
        public string Phrase;
        public int PhraseTime;

        public DialogueTextData(string phraseID = " ", string characterName = " ", string phrase = " ", int phraseTime = 1)
        {
            PhraseID = phraseID;
            CharacterName = characterName;
            Phrase = phrase;
            PhraseTime = phraseTime;
        }
    }
}
