using System;
using UnityEngine;
using UnityEngine.Events;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphEvent : MonoBehaviour
    {
        public string EventID => _eventID;
        [SerializeField] private DialogueGraphRunner runner;
        [SerializeField] private string _eventID;
        
        [Space(20)]
        [SerializeField] private UnityEvent _event;

        private void Awake()
        {
            runner.AddEvent(this);
        }

        public void CallEvent()
        {
            _event?.Invoke();
        }
    }
}
