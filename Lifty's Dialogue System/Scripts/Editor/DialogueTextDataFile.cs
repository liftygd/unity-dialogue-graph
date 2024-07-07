using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueTextDataFile : MonoBehaviour
    {
        [MenuItem("Assets/Create/Lifty's Dialogue/New Dialogue Text File", priority = 100)]
        private static void CreateNewTextFile()
        {
            string folderGUID = Selection.assetGUIDs[0];
            string projectFolderPath = AssetDatabase.GUIDToAssetPath(folderGUID);
            string folderDirectory = Path.GetFullPath(projectFolderPath);
 
            using (StreamWriter sw = File.CreateText(folderDirectory + "/New Dialogue Text.json"))
            {
                DialogueTextData[] template = new[] { new DialogueTextData() };
                var jsonTemplate = JsonHelper.ToJson(template, true);

                sw.WriteLine(jsonTemplate);
            }
       
            AssetDatabase.Refresh();
        }
    }
}

#endif
