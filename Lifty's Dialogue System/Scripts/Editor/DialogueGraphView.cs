using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace Lifty.DialogueSystem.Editor
{
    public class DialogueGraphView : GraphView
    {
        private DialogueGraphAsset _dialogueGraph;
        private SerializedObject _serializedObject;
        private DialogueGraphEditorWindow _window;

        public DialogueGraphEditorWindow Window => _window;

        public List<DialogueGraphEditorNode> _graphNodes;
        public Dictionary<string, DialogueGraphEditorNode> _nodeDictionary;

        private DialogueGraphWindowSearchProvider _searchProvider;
        private Dictionary<string, Port> _allPorts;

        public DialogueGraphView(SerializedObject serializedObject, DialogueGraphEditorWindow window)
        {
            _serializedObject = serializedObject;
            _dialogueGraph = (DialogueGraphAsset) _serializedObject.targetObject;
            _window = window;

            _graphNodes = new List<DialogueGraphEditorNode>();
            _nodeDictionary = new Dictionary<string, DialogueGraphEditorNode>();

            _searchProvider = ScriptableObject.CreateInstance<DialogueGraphWindowSearchProvider>();
            _searchProvider.Graph = this;
            this.nodeCreationRequest = ShowSearchWindow;

            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Plugins/Lifty's Dialogue System/Scripts/Editor/USS/DialogueGraphEditor.uss");
            styleSheets.Add(style);

            GridBackground bg = new GridBackground();
            bg.name = "Grid";
            Add(bg);
            bg.SendToBack();

            LoadView();

            this.RegisterCallback<MouseUpEvent>((e) => _dialogueGraph.GraphViewPosition = this.contentViewContainer.transform.position);
            this.RegisterCallback<WheelEvent>((e) => _dialogueGraph.GraphViewScale = this.contentViewContainer.transform.scale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ClickSelector());
            this.AddManipulator(new ContentZoomer());

            DrawNodes();
            DrawConnections();

            graphViewChanged += OnGraphViewChanged;
        }

        private void LoadView()
        {
            this.contentViewContainer.transform.position = _dialogueGraph.GraphViewPosition;
            this.contentViewContainer.transform.scale = _dialogueGraph.GraphViewScale;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var allPorts = new List<Port>();
            var ports = new List<Port>();

            foreach (var node in _graphNodes)
            {
                allPorts.AddRange(node.Ports.Values);
            }

            foreach (var port in allPorts)
            {
                if (port == startPort) continue;
                if (port.node == startPort.node) continue;
                if (port.direction == startPort.direction) continue;
                if (port.portType == typeof(DialogueGraphPortTypes.EmptyPort)) continue;
                if (port.portType == startPort.portType) ports.Add(port);
            }
            
            return ports;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.movedElements != null)
            {
                RecordUndo(_serializedObject.targetObject, "Moved Elements In Graph");
                List<DialogueGraphEditorNode> nodes = graphViewChange.movedElements.OfType<DialogueGraphEditorNode>().ToList();

                foreach (var node in nodes)
                {
                    node.SavePosition();
                }
            }
            
            if (graphViewChange.elementsToRemove != null)
            {
                RecordUndo(_serializedObject.targetObject, "Removed Elements From Graph");
                List<DialogueGraphEditorNode> nodes = graphViewChange.elementsToRemove.OfType<DialogueGraphEditorNode>().ToList();

                if (nodes.Count > 0)
                {
                    for (int i = nodes.Count - 1; i >= 0; i--)
                    {
                        RemoveNode(nodes[i]);
                    }
                }
                
                List<Edge> edges = graphViewChange.elementsToRemove.OfType<Edge>().ToList();

                if (edges.Count > 0)
                {
                    for (int i = edges.Count - 1; i >= 0; i--)
                    {
                        RemoveEdge(edges[i]);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                RecordUndo(_serializedObject.targetObject, "Added Connection");
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    CreateEdge(edge);
                }
            }

            return graphViewChange;
        }

        private void GetAllPorts()
        {
            _allPorts = new Dictionary<string, Port>();
            
            foreach (var node in _graphNodes)
            {
                foreach (var port in node.Ports)
                {
                    _allPorts.Add(port.Value.name, port.Value);
                }
            }
        }

        private void CreateEdge(Edge edge)
        {
            var outputNode = (DialogueGraphEditorNode) edge.output.node;
            var inputNode = (DialogueGraphEditorNode) edge.input.node;
            
            outputNode.ConnectNode(edge.output.name, inputNode.Node);
            inputNode.ConnectNode(edge.input.name, outputNode.Node);
            
            _dialogueGraph.AddConnection(edge.output.name, edge.input.name);
        }

        private void RemoveEdge(Edge edge)
        {
            var outputNode = (DialogueGraphEditorNode) edge.output.node;
            var inputNode = (DialogueGraphEditorNode) edge.input.node;
            
            outputNode.DisconnectNode(edge.output.name);
            inputNode.DisconnectNode(edge.input.name);
            
            _dialogueGraph.RemoveConnection(edge.output.name);
        }

        private void RemoveNode(DialogueGraphEditorNode node)
        {
            _dialogueGraph.Nodes.Remove(node.Node);
            _nodeDictionary.Remove(node.Node.ID);
            _graphNodes.Remove(node);

            foreach (var port in node.Ports)
            {
                _dialogueGraph.RemoveConnection(port.Value.name);
            }
            
            GetAllPorts();
            _serializedObject.Update();
        }

        private void DrawNodes()
        {
            foreach (var node in _dialogueGraph.Nodes)
            {
                AddNodeToGraph(node);
            }
        }

        private void DrawConnections()
        {
            GetAllPorts();
            List<string> _portsToRemove = new List<string>();
            
            foreach (var connectedPort in _dialogueGraph.ConnectedPorts)
            {
                if (!_allPorts.ContainsKey(connectedPort.OutPortID) || !_allPorts.ContainsKey(connectedPort.InPortID))
                {
                    Debug.LogError("DIALOGUE GRAPH: It seems some port nodes were renamed or something else happened, so their connections got lost!");
                    _portsToRemove.Add(connectedPort.OutPortID);
                    continue;
                }
                
                Port outPort = _allPorts[connectedPort.OutPortID];
                Port inPort = _allPorts[connectedPort.InPortID];

                Edge edge = outPort.ConnectTo(inPort);
                AddElement(edge);
            }
            
            _portsToRemove.ForEach(port => _dialogueGraph.RemoveConnection(port));
            _graphNodes.ForEach(x => x.UpdatePorts());
        }

        private void ShowSearchWindow(NodeCreationContext obj)
        {
            _searchProvider.Target = (VisualElement) focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), _searchProvider);
        }

        public void Add(DialogueGraphNode node)
        {
            RecordUndo(_serializedObject.targetObject, "Added Node");
            _dialogueGraph.Nodes.Add(node);
            _serializedObject.Update();
            
            AddNodeToGraph(node);
        }

        public void RecordUndo(UnityEngine.Object obj, string message)
        {
            Undo.RecordObject(obj, message);
        }

        private void AddNodeToGraph(DialogueGraphNode node)
        {
            node.TypeName = node.GetType().AssemblyQualifiedName;

            DialogueGraphEditorNode editorNode = new DialogueGraphEditorNode(node, this);
            editorNode.SetPosition(node.Position);
            
            _graphNodes.Add(editorNode);
            _nodeDictionary.Add(node.ID, editorNode);
            AddElement(editorNode);
        }
    }
}
