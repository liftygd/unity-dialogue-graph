using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lifty.DialogueSystem.Editor
{
    public struct SearchContextElement
    {
        public object target { get; private set; }
        public string title { get; private set; }

        public SearchContextElement(object target, string title)
        {
            this.target = target;
            this.title = title;
        }
    }
    
    public class DialogueGraphWindowSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public DialogueGraphView Graph;
        public VisualElement Target;
        public static List<SearchContextElement> Elements;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> tree = new List<SearchTreeEntry>();
            tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 0));

            Elements = new List<SearchContextElement>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.CustomAttributes.ToList() == null) continue;

                    var attribute = type.GetCustomAttribute(typeof(NodeInfoAttribute));
                    if (attribute == null) continue;

                    NodeInfoAttribute att = (NodeInfoAttribute) attribute;
                    var node = Activator.CreateInstance(type);

                    if (string.IsNullOrEmpty(att.MenuItem)) continue;
                    Elements.Add(new SearchContextElement(node, att.MenuItem));
                }
            }
            
            Elements.Sort((entry1, entry2) =>
            {
                string[] splits1 = entry1.title.Split('/');
                string[] splits2 = entry2.title.Split('/');

                for (int i = 0; i < splits1.Length; i++)
                {
                    if (i >= splits2.Length) return 1;

                    int value = splits1[i].CompareTo(splits2[i]);
                    if (value == 0) continue;

                    if (splits1.Length != splits2.Length && (i == splits1.Length - 1 || i == splits2.Length - 1))
                        return splits1.Length < splits2.Length ? 1 : -1;

                    return value;
                }

                return 0;
            });

            List<string> groups = new List<string>();
            foreach (var element in Elements)
            {
                string[] entryTitle = element.title.Split('/');
                string groupName = "";

                for (int i = 0; i < entryTitle.Length - 1; i++)
                {
                    groupName += entryTitle[i];
                    if (!groups.Contains(groupName))
                    {
                        tree.Add(new SearchTreeGroupEntry(new GUIContent("> " + entryTitle[i]), i + 1));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                var nodeSearchTitle = "    " + entryTitle.Last();

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(nodeSearchTitle));
                entry.level = entryTitle.Length;
                entry.userData = new SearchContextElement(element.target, element.title);
                tree.Add(entry);
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var windowMousePosition = Graph.ChangeCoordinatesTo(Graph, context.screenMousePosition - Graph.Window.position.position);
            var graphMousePosition = Graph.contentViewContainer.WorldToLocal(windowMousePosition);

            SearchContextElement element = (SearchContextElement) SearchTreeEntry.userData;

            DialogueGraphNode node = (DialogueGraphNode) element.target;
            node.SetPosition(new Rect(graphMousePosition, new Vector2()));
            Graph.Add(node);

            return true;
        }
    }
}
