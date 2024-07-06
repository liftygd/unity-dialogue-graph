using System;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class NodeInfoAttribute : Attribute
    {
        public string Title => _nodeTitle;
        private string _nodeTitle;
        
        public string MenuItem => _menuItem;
        private string _menuItem;

        public string TypeClass => _typeClass;
        private string _typeClass;

        public NodeInfoAttribute(string title, string menuItem = "", string typeClass = "")
        {
            _nodeTitle = title;
            _menuItem = menuItem;
            _typeClass = typeClass;
        }
    }
}
