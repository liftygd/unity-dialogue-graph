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
            var startNode = _dialogueGraph.GetStartNode();
            startNode.Process(this);
        }

        public T GetVariableValue<T>(string variableName)
        {
            return (T) DialogueGraphController.Instance.GetVariableValue<T>(variableName);
        }

        public void SetVariableValue<T>(string variableName, T variableValue)
        {
            DialogueGraphController.Instance.SetVariableValue<T>(variableName, variableValue);
        }
    }
}
