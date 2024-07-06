using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphRunner : MonoBehaviour
    {
        [SerializeField] private DialogueGraphAsset _dialogueGraph;
        [SerializeField] private bool _runOnStart;

        private void Start()
        {
            if (_runOnStart) StartDialogue();
        }

        public void StartDialogue()
        {
            _dialogueGraph.GetStartNode().Process();
        }
    }
}
