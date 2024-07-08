using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueGraphEditorNode : Node
    {
        public DialogueGraphNode Node => _node;
        private DialogueGraphNode _node;

        public Dictionary<string, Port> Ports => _ports;
        private Dictionary<string, Port> _ports;

        private Dictionary<string, VisualElement> _fields;

        public DialogueGraphEditorNode(DialogueGraphNode node)
        {
            this.AddToClassList("dialogue-node");
            this.RemoveFromClassList("graphElement");
            this.RemoveFromClassList("node");

            _node = node;

            Type typeInfo = node.GetType();
            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            this.titleContainer.name = "NodeTitle";
            this.titleContainer.AddToClassList("dialogue-node-title");
            this.titleContainer.AddToClassList(info.TypeClass);

            title = info.Title;

            inputContainer.name = "in-ports";
            outputContainer.name = "out-ports";
            
            string[] depths = info.MenuItem.Split("/");
            foreach (var depth in depths)
            {
                this.AddToClassList(depth.ToLower().Replace(" ", "-"));
            }
            
            this.name = typeInfo.Name;

            _ports = new Dictionary<string, Port>();
            _fields = new Dictionary<string, VisualElement>();

            var fields = typeInfo.GetFields();
            foreach (var field in fields)
            {
                var nodeFlowInfo = field.GetCustomAttribute<NodeFlowAttribute>();
                if (nodeFlowInfo == null) continue;
                
                CreateFlowPort(nodeFlowInfo, field.Name);
            }
        }

        public void ConnectNode(string portName, DialogueGraphNode node)
        {
            var fieldName = portName.Split("_")[1];
            
            Type typeInfo = _node.GetType();
            var fields = typeInfo.GetFields();
            foreach (var field in fields)
            {
                var nodeFlowInfo = field.GetCustomAttribute<NodeFlowAttribute>();
                if (nodeFlowInfo == null) continue;
                if (field.Name != fieldName) continue;
                
                field.SetValue(_node, node);
                
                if (!_fields.ContainsKey(nodeFlowInfo.PortName)) continue;
                
                var port = _ports[nodeFlowInfo.PortName];
                port.Remove(_fields[nodeFlowInfo.PortName]);
                _fields.Remove(nodeFlowInfo.PortName);
            }
        }
        
        public void DisconnectNode(string portName)
        {
            var fieldName = portName.Split("_")[1];
            
            Type typeInfo = _node.GetType();
            var fields = typeInfo.GetFields();
            foreach (var field in fields)
            {
                var nodeFlowInfo = field.GetCustomAttribute<NodeFlowAttribute>();
                if (nodeFlowInfo == null) continue;
                if (field.Name != fieldName) continue;
                
                field.SetValue(_node, null);

                var port = _ports[nodeFlowInfo.PortName];
                port.Add(CreatePortField(nodeFlowInfo));
            }
        }

        public void UpdatePorts()
        {
            foreach (var port in _ports)
            {
                if (!port.Value.connected) continue;
                if (!_fields.ContainsKey(port.Value.portName)) continue;

                var portToClear = port.Value;
                portToClear.Remove(_fields[portToClear.portName]);
                _fields.Remove(portToClear.portName);
            }
        }

        public void SavePosition()
        {
            _node.SetPosition(GetPosition());
        }

        private void CreateFlowPort(NodeFlowAttribute flowInfo, string fieldName)
        {
            var portDirection = flowInfo.FlowType == NodeFlowType.FlowInput ? Direction.Input : Direction.Output;
            var capacity = flowInfo.PortType == typeof(DialogueGraphPortTypes.FlowPort) && flowInfo.FlowType == NodeFlowType.FlowInput ? Port.Capacity.Multi : Port.Capacity.Single;

            var newPort = InstantiatePort(
                Orientation.Horizontal, 
                portDirection, 
                capacity,
                flowInfo.PortType);

            newPort.portName = flowInfo.PortName;
            newPort.tooltip = flowInfo.ToolTip;
            newPort.name = _node.ID + "_" + fieldName;

            newPort.Add(CreatePortField(flowInfo));

            var portType = (DialogueGraphPortTypes.DialogueGraphPort) Activator.CreateInstance(flowInfo.PortType, new object[]{});
            newPort.portColor = portType.PortColor;
            newPort.AddToClassList(portType.PortClass);

            _ports.Add(newPort.portName, newPort);
            
            if (portDirection == Direction.Input)
                inputContainer.Add(newPort);
            else
                outputContainer.Add(newPort);
        }

        private VisualElement CreatePortField(NodeFlowAttribute flowInfo)
        {
            Type typeInfo = _node.GetType();
            var fields = typeInfo.GetFields();
            foreach (var field in fields)
            {
                var nodeFlowFieldInfo = field.GetCustomAttribute<NodeFlowFieldAttribute>();
                if (nodeFlowFieldInfo == null) continue;
                if (nodeFlowFieldInfo.PortName != flowInfo.PortName) continue;

                var valueField = GetFieldByType(field);
                
                _fields.Add(flowInfo.PortName, valueField);
                return valueField;
            }

            return null;
        }

        private VisualElement GetFieldByType(FieldInfo field)
        {
            if (field.FieldType == typeof(string))
            {
                var valueField = new TextField();

                valueField.value = (string) field.GetValue(_node);
                valueField.RegisterValueChangedCallback(evt =>
                {
                    field.SetValue(_node, valueField.value);
                    DialogueGraphEditorWindow.UnsavedChanges();
                });

                return valueField;
            }
            
            if (field.FieldType == typeof(int))
            {
                var valueField = new IntegerField();
                
                valueField.value = (int) field.GetValue(_node);
                valueField.RegisterValueChangedCallback(evt =>
                {
                    field.SetValue(_node, valueField.value);
                    DialogueGraphEditorWindow.UnsavedChanges();
                });

                return valueField;
            }
            
            if (field.FieldType == typeof(bool))
            {
                var valueField = new Toggle();
                
                valueField.value = (bool) field.GetValue(_node);
                valueField.RegisterValueChangedCallback(evt =>
                {
                    field.SetValue(_node, valueField.value);
                    DialogueGraphEditorWindow.UnsavedChanges();
                });

                return valueField;
            }
            
            return null;
        }
    }
}
