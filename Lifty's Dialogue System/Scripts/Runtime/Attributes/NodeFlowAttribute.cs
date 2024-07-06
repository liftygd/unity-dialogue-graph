using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem
{
    public enum NodeFlowType
    {
        FlowInput,
        FlowOutput
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NodeFlowAttribute : Attribute
    {
        public string PortName => _portName;
        private string _portName;
        
        public NodeFlowType FlowType => _nodeFlowType;
        private NodeFlowType _nodeFlowType;

        public Type PortType => _portType;
        private Type _portType;

        public string ToolTip => _toolTip;
        private string _toolTip;
        
        public NodeFlowAttribute(string portName, NodeFlowType nodeFlowType, Type portType = null, string toolTip = "")
        {
            _portName = portName;
            _nodeFlowType = nodeFlowType;

            if (portType == null)
                _portType = typeof(DialogueGraphPortTypes.FlowPort);
            else
                _portType = portType;

            _toolTip = toolTip;
        }
    }
}
