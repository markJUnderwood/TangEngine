using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TangAI.Behavior
{
    internal class Blackboard : Dictionary<string, object>
    {
        private readonly Dictionary<string, TreeMemory> _treeMemory;

        public Blackboard()
        {
            _treeMemory = new Dictionary<string, TreeMemory>();
        }
        [DebuggerStepThrough]
        public T GetValue<T>(string key, string treeScope, string nodeScope)
        {
            Dictionary<string, object> nodeVariables = GetTreeScope(treeScope)[nodeScope];
            if (!nodeVariables.ContainsKey(key))
                nodeVariables[key] = default(T);
            return (T) nodeVariables[key];
        }
        [DebuggerStepThrough]
        public void SetValue<T>(T value, string key, string treeScope, string nodeScope)
        {
            GetTreeScope(treeScope)[nodeScope][key] = value;
        }
        
        [DebuggerStepThrough]
        public TreeMemory GetTreeScope(string treeScope)
        {
            if (!_treeMemory.ContainsKey(treeScope))
                _treeMemory.Add(treeScope, new TreeMemory());
            return _treeMemory[treeScope];
        }

        public void SetValue<T>(T value, string key)
        {
            this[key] = value;
        }

        public T GetValue<T>(string key)
        {
            if (!ContainsKey(key))
                this[key] = default(T);
            return (T) this[key];
        }
    }
}