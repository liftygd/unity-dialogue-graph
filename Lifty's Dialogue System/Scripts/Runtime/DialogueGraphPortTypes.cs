using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueGraphPortTypes
    {
        public class DialogueGraphPort
        {
            public Color PortColor;
            protected virtual string portColorHex => "#FFFFFF";
            
            public virtual string PortClass => "";

            public DialogueGraphPort()
            {
                ColorUtility.TryParseHtmlString(portColorHex , out Color portColor );
                PortColor = portColor;
            }
        }

        public class EmptyPort : DialogueGraphPort
        {
            protected override string portColorHex => "#00000000";
            public override string PortClass => "invisible-connector";
        };
        
        public class FlowPort : DialogueGraphPort { protected override string portColorHex => "#a8a8a8"; };

        public class StringPort : DialogueGraphPort { protected override string portColorHex => "#55a6e0"; };
        
        public class IntegerPort : DialogueGraphPort { protected override string portColorHex => "#5db374"; };
        
        public class BooleanPort : DialogueGraphPort { protected override string portColorHex => "#b35d5d"; };
    }
}
