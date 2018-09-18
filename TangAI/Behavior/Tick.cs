using System;
using System.Collections.Generic;
using System.Diagnostics;
using TangAI.Behavior.Nodes;

namespace TangAI.Behavior
{
    public class Tick
    {
        public List<BaseNode> _nodes;

        public Tick()
        {
            _nodes = new List<BaseNode>();
            Tree = new BehaviorTree(new StateReturnNode());
            BlackBoard = new Blackboard();
            Target = new object();
        }

        public BehaviorTree Tree { get; set; }
        internal Blackboard BlackBoard { get; set; }
        public object Target { get; set; }

        public event EventHandler<BaseNode> NodeOpen;
        public event EventHandler<BaseNode> NodeTick;
        public event EventHandler<BaseNode> NodeClose;
        public event EventHandler<BaseNode> NodeEnter;
        public event EventHandler<BaseNode> NodeExit;
        [DebuggerStepThrough]
        public void OpenNode(BaseNode node)
        {
            OnNodeOpen(node);
        }
        [DebuggerStepThrough]
        public void TickNode(BaseNode node)
        {
            OnNodeTick(node);
        }
        [DebuggerStepThrough]
        public void CloseNode(BaseNode node)
        {
            OnNodeClose(node);
            _nodes.RemoveAt(_nodes.Count - 1);
        }
        [DebuggerStepThrough]
        public void EnterNode(BaseNode node)
        {
            OnNodeEnter(node);
            _nodes.Add(node);
        }
        [DebuggerStepThrough]
        public void ExitNode(BaseNode node)
        {
            OnNodeExit(node);
        }
        [DebuggerStepThrough]
        private void OnNodeOpen(BaseNode e)
        {
            NodeOpen?.Invoke(this, e);
        }
        [DebuggerStepThrough]
        private void OnNodeTick(BaseNode e)
        {
            NodeTick?.Invoke(this, e);
        }
        [DebuggerStepThrough]
        private void OnNodeClose(BaseNode e)
        {
            NodeClose?.Invoke(this, e);
        }
        [DebuggerStepThrough]
        private void OnNodeEnter(BaseNode e)
        {
            NodeEnter?.Invoke(this, e);
        }
        [DebuggerStepThrough]
        private void OnNodeExit(BaseNode e)
        {
            NodeExit?.Invoke(this, e);
        }
    }
}