using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Lifty.DialogueSystem.Editor
{
    [CustomEditor(typeof(DialogueGraphAsset))]
    public class DialogueGraphAssetEditor : UnityEditor.Editor
    {
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int index)
        {
            Object asset = EditorUtility.InstanceIDToObject(instanceID);
            if (asset.GetType() == typeof(DialogueGraphAsset))
            {
                DialogueGraphEditorWindow.Open((DialogueGraphAsset) asset);
                return true;
            }

            return false;
        }
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Graph"))
            {
                DialogueGraphEditorWindow.Open((DialogueGraphAsset) target);
            }
        }
    }
}
