using UnityEngine;
using UnityEngine.Events;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphEvent : MonoBehaviour
    {
        public string EventID => _eventID;
        [SerializeField] private string _eventID;
        
        [Space(20)]
        [SerializeField] private UnityEvent _event;

        public void CallEvent()
        {
            _event?.Invoke();
        }
    }
}
