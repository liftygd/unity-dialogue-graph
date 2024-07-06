using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NodeFlowFieldAttribute : Attribute
    {
        public string PortName => _portName;
        private string _portName;

        public NodeFlowFieldAttribute(string portName)
        {
            _portName = portName;
        }
    }
}
