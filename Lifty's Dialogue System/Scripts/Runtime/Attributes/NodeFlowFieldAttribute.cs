using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NodeFlowFieldAttribute : Attribute
    {
        public string PortName => _portName;
        private string _portName;

        public Type FieldType => _fieldType;
        private Type _fieldType;

        public NodeFlowFieldAttribute(string portName, Type fieldType)
        {
            _portName = portName;
            _fieldType = fieldType;
        }
    }
}
