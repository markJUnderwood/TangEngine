using System;
using System.Diagnostics;

namespace TangAI.Behavior.Nodes
{
    public abstract class Decorator : BaseNode
    {
        [DebuggerStepThrough]
        public Decorator(string id, BaseNode child) : base(id)
        {
            Child = child;
        }
        [DebuggerStepThrough]
        public Decorator(BaseNode child) : this(Guid.NewGuid().ToString("N"), child)
        {
        }
        [DebuggerStepThrough]
        public Decorator() : this(null)
        {
        }

        public BaseNode Child { get; set; }
    }

    public class Inverter : Decorator
    {
        protected override BehaviorState OnTick(Tick tick)
        {
            if (Child == null)
            {
                tick.BlackBoard.SetValue( "Child Node is Null",ErrorKey,tick.Tree.Id,Id);
                return BehaviorState.Error;
            }
            BehaviorState state = Child.Execute(tick);
            switch (state)
            {
                case BehaviorState.Success:
                    state = BehaviorState.Failure;
                    break;
                case BehaviorState.Failure:
                    state = BehaviorState.Success;
                    break;
            }
            return state;
        }
    }
}