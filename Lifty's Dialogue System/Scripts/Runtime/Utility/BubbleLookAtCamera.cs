using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class BubbleLookAtCamera : MonoBehaviour
    {
        private Transform _camTransform;

        private void Start()
        {
            _camTransform = Camera.main.transform;
        }

        private void Update()
        {
            var cameraPos = new Vector3(_camTransform.position.x, transform.position.y, _camTransform.position.z);
            transform.LookAt(cameraPos);
        }
    }
}
