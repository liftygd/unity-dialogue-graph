using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class BubbleLookAtCamera : MonoBehaviour
    {
        private Transform _camTransform;

        private void Start()
        {
            if (Camera.main != null)
                _camTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (Camera.main == null) return;
            
            if (_camTransform == null)
            {
                _camTransform = Camera.main.transform;
                return;
            }

            var cameraPos = new Vector3(_camTransform.position.x, transform.position.y, _camTransform.position.z);
            transform.LookAt(cameraPos);
        }
    }
}
