using System.Collections.Generic;
using System.Diagnostics;
using TangAI.Behavior.Nodes;

namespace TangAI.Behavior
{
    public class TreeMemory
    {
        [DebuggerStepThrough]
        public TreeMemory()
        {
            NodeMemory = new Dictionary<string, Dictionary<string, object>>();
            Nodes = new List<BaseNode>();
        }

        public Dictionary<string, Dictionary<string, object>> NodeMemory { get; set; }
        public List<BaseNode> Nodes { get; set; }

        public Dictionary<string, object> this[string nodeScope] => GetNodeMemory(nodeScope);


        private Dictionary<string, object> GetNodeMemory(string nodeScope)
        {
            if (!NodeMemory.ContainsKey(nodeScope))
                NodeMemory.Add(nodeScope, new Dictionary<string, object>());
            return NodeMemory[nodeScope];
        }
    }
}