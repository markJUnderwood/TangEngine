using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TangAI.Behavior.Nodes
{
    public abstract class CompositeNode : BaseNode
    {
        protected const string RunningChildKey = "runningChild";
        [DebuggerStepThrough]
        public CompositeNode(string id, IList<BaseNode> children) : base(id)
        {
            Children = children;
        }
        [DebuggerStepThrough]
        public CompositeNode() : this(Guid.NewGuid().ToString("N"))
        {
        }
        [DebuggerStepThrough]
        public CompositeNode(string id) : this(id, new List<BaseNode>())
        {
        }
        [DebuggerStepThrough]
        public CompositeNode(IList<BaseNode> children) : this(Guid.NewGuid().ToString("N"), children)
        {
        }

        public IList<BaseNode> Children { get; set; }
    }

    /// <summary>
    ///     Sequence Node ticks it's children sequentially until one of them returns <see cref="F:BehaviorState.Failure" />,
    ///     <see cref="F:BehaviorState.Running" /> or <see cref="F:BehaviorState.Error" />.
    ///     If all children return the <see cref="F:BehaviorState.Success" /> state the sequence also returns
    ///     <see cref="F:BehaviorState.Success" />
    /// </summary>
    public class Sequence : CompositeNode
    {
        [DebuggerStepThrough]
        public Sequence()
        {
        }
        [DebuggerStepThrough]
        public Sequence(string id) : base(id)
        {
        }
        [DebuggerStepThrough]
        public Sequence(IList<BaseNode> children) : base(children)
        {
        }
        [DebuggerStepThrough]
        public Sequence(string id, IList<BaseNode> children) : base(id, children)
        {
        }
        protected override BehaviorState OnTick(Tick tick)
        {
            foreach (BaseNode child in Children)
            {
                BehaviorState state = child.Execute(tick);
                if (state != BehaviorState.Success)
                    return state;
            }
            return BehaviorState.Success;
        }
    }

    public class Selector : CompositeNode
    {
        [DebuggerStepThrough]
        public Selector()
        {
        }
        [DebuggerStepThrough]
        public Selector(string id) : base(id)
        {
        }
        [DebuggerStepThrough]
        public Selector(IList<BaseNode> children) : base(children)
        {
        }

        public Selector(string id, IList<BaseNode> children) : base(id, children)
        {
        }

        protected override BehaviorState OnTick(Tick tick)
        {
            foreach (BaseNode child in Children)
            {
                BehaviorState state = child.Execute(tick);
                if (state != BehaviorState.Failure)
                    return state;
            }
            return BehaviorState.Failure;
        }
    }

    public class MemSequence : CompositeNode
    {
        [DebuggerStepThrough]
        public MemSequence()
        {
        }
        [DebuggerStepThrough]
        public MemSequence(string id) : base(id)
        {
        }
        [DebuggerStepThrough]
        public MemSequence(IList<BaseNode> children) : base(children)
        {
        }
        [DebuggerStepThrough]
        public MemSequence(string id, IList<BaseNode> children) : base(id, children)
        {
        }

        protected override void OnOpen(Tick tick)
        {
            tick.BlackBoard.SetValue(0,RunningChildKey,tick.Tree.Id,Id);
        }

        protected override BehaviorState OnTick(Tick tick)
        {
            int runningChild = tick.BlackBoard.GetValue<int>(RunningChildKey,tick.Tree.Id,Id);
            for (int i = runningChild; i < Children.Count; i++)
            {
                BehaviorState state = Children[i].Execute(tick);
                if (state != BehaviorState.Success)
                {
                    if (state == BehaviorState.Running)
                        tick.BlackBoard.SetValue(i,RunningChildKey,tick.Tree.Id,Id);
                    return state;
                }
            }
            return BehaviorState.Success;
        }
    }

    public class MemSelector : CompositeNode
    {
        [DebuggerStepThrough]
        public MemSelector()
        {
        }
        [DebuggerStepThrough]
        public MemSelector(string id) : base(id)
        {
        }
        [DebuggerStepThrough]
        public MemSelector(IList<BaseNode> children) : base(children)
        {
        }
        [DebuggerStepThrough]
        public MemSelector(string id, IList<BaseNode> children) : base(id, children)
        {
        }

        protected override void OnOpen(Tick tick)
        {
            tick.BlackBoard.SetValue(0,RunningChildKey,tick.Tree.Id,Id);
        }

        protected override BehaviorState OnTick(Tick tick)
        {
            int runningChild = tick.BlackBoard.GetValue<int>(RunningChildKey,tick.Tree.Id,Id);
            for (int i = runningChild; i < Children.Count; i++)
            {
                BehaviorState state = Children[i].Execute(tick);
                if (state != BehaviorState.Failure)
                {
                    if (state == BehaviorState.Running)
                        tick.BlackBoard.SetValue(i,RunningChildKey,tick.Tree.Id,Id);
                    return state;
                }
            }
            return BehaviorState.Failure;
        }
    }
}