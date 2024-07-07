using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue Text File Container", menuName = "Lifty's Dialogue/New Dialogue Text File Container")]
    public class DialogueFileData : ScriptableObject
    {
        public List<DialogueLocalizedFile> DialogueFiles = new List<DialogueLocalizedFile>();
    }

    [Serializable]
    public class DialogueLocalizedFile
    {
        public DialogueLanguageEnum Language;
        public TextAsset DialogueTextFile;
    }
}
