using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueGraphEditorToolbar
    {
        private Toolbar _toolbar;
        
        public DialogueGraphEditorToolbar()
        {
            _toolbar = new Toolbar();
            _toolbar.AddToClassList("dialogue-graph-toolbar");

            Button undoButton = new Button();
            undoButton.text = "Undo";
            undoButton.AddToClassList("dialogue-graph-toolbar-button");
            undoButton.clicked += () => Undo.PerformUndo();
            _toolbar.Add(undoButton);

            Button redoButton = new Button();
            redoButton.text = "Redo";
            redoButton.AddToClassList("dialogue-graph-toolbar-button");
            redoButton.clicked += () => Undo.PerformRedo();
            _toolbar.Add(redoButton);
        }

        public Toolbar GetToolbar()
        {
            return _toolbar;
        }
    }
}
