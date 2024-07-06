using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueGraphEditorWindow : EditorWindow
    {
        public DialogueGraphAsset CurrentGraph => _currentGraph;
        [SerializeField] private DialogueGraphAsset _currentGraph;
        [SerializeField] private DialogueGraphView _currentView;
        [SerializeField] private SerializedObject _serializedObject;

        private static DialogueGraphEditorWindow ThisWindow;

        public static void Open(DialogueGraphAsset target)
        {
            DialogueGraphEditorWindow[] windows = Resources.FindObjectsOfTypeAll<DialogueGraphEditorWindow>();
            foreach (var window in windows)
            {
                if (window.CurrentGraph != target) continue;
                
                window.Focus();
                return;
            }

            DialogueGraphEditorWindow newWindow = CreateWindow<DialogueGraphEditorWindow>(
                typeof(DialogueGraphEditorWindow), 
                typeof(SceneView));

            newWindow.titleContent = new GUIContent($"{target.name}");
            newWindow.Load(target);
        }

        private void OnEnable()
        {
            if (_currentGraph == null) return;
            
            ThisWindow = this;
            DrawGraph();
        }

        private void OnGUI()
        {
            if (_currentGraph == null) return;
            
            hasUnsavedChanges = EditorUtility.IsDirty(_currentGraph);
        }

        public void Load(DialogueGraphAsset target)
        {
            _currentGraph = target;
            DrawGraph();
        }

        private void DrawGraph()
        {
            _serializedObject = new SerializedObject(_currentGraph);
            _currentView = new DialogueGraphView(_serializedObject, this);
            _currentView.graphViewChanged += OnChanged;
            
            rootVisualElement.Add(_currentView);
        }

        private GraphViewChange OnChanged(GraphViewChange graphViewChange)
        {
            UnsavedChanges();

            return graphViewChange;
        }

        public static void UnsavedChanges()
        {
            ThisWindow.hasUnsavedChanges = true;
            EditorUtility.SetDirty(ThisWindow._currentGraph);
        }
    }
}
