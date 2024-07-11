using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Text File Container", menuName = "Lifty's Dialogue/New Dialogue Text File Container")]
    public class DialogueFileData : ScriptableObject
    {
        public List<DialogueLocalizedFile> DialogueFiles = new List<DialogueLocalizedFile>();

        public DialogueLocalizedFile GetFileByLanguage(DialogueLanguageEnum language)
        {
            return DialogueFiles.FirstOrDefault(file => file.Language == language);
        }
    }

    [Serializable]
    public class DialogueLocalizedFile
    {
        public DialogueLanguageEnum Language;
        public TextAsset DialogueTextFile;
    }

    [Serializable]
    public class DialogueCharacterBubble
    {
        public string CharacterID;
        public TMP_Text TextUI;
    }
}
