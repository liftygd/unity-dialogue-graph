using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueGraphEditorToolbar
    {
        private Toolbar _toolbar;

        private bool _blackboardActive;
        
        public DialogueGraphEditorToolbar(Blackboard blackboard)
        {
            _toolbar = new Toolbar();
            _toolbar.AddToClassList("dialogue-graph-toolbar");

            Button blackBoardButton = new Button();
            blackBoardButton.text = "Variable Blackboard";
            blackBoardButton.AddToClassList("dialogue-graph-toolbar-button");
            blackBoardButton.clicked += () => ChangeBlackboardState(blackboard);
            _toolbar.Add(blackBoardButton);
        }

        private void ChangeBlackboardState(Blackboard blackboard)
        {
            _blackboardActive = !_blackboardActive;
            
            if (_blackboardActive)
                blackboard.RemoveFromClassList("disable-element");
            else
                blackboard.AddToClassList("disable-element");
        }

        public Toolbar GetToolbar()
        {
            return _toolbar;
        }
    }
}
