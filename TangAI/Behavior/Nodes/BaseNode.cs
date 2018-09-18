using System;
using System.Diagnostics;

namespace TangAI.Behavior.Nodes
{
    public abstract class BaseNode : IEquatable<BaseNode>
    {
        private const string IsOpenKey = "isOpen";
        protected const string ErrorKey = "Error";
        [DebuggerStepThrough]
        protected BaseNode(string id)
        {
            Id = id;
        }
        [DebuggerStepThrough]
        public BaseNode():this(Guid.NewGuid().ToString("N"))
        {
            
        }

        public string Id { get; }

        public BehaviorState Status { get; set; }
        [DebuggerStepThrough]
        public bool Equals(BaseNode other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || string.Equals(Id, other.Id));
        }

        protected internal BehaviorState Execute(Tick tick)
        {
            Enter(tick);
            if (!tick.BlackBoard.GetValue<bool>(IsOpenKey,tick.Tree.Id,Id))
                Open(tick);

            Status = Tick(tick);
            if (Status != BehaviorState.Running)
                Close(tick);
            Exit(tick);
            return Status;
        }
        [DebuggerStepThrough]
        protected virtual void OnClose(Tick tick)
        {
        }
        [DebuggerStepThrough]
        protected virtual void OnEnter(Tick tick)
        {
        }
        [DebuggerStepThrough]
        protected virtual void OnOpen(Tick tick)
        {
        }
        [DebuggerStepThrough]
        protected virtual BehaviorState OnTick(Tick tick)
        {
            return BehaviorState.Success;
        }
        [DebuggerStepThrough]
        protected virtual void OnExit(Tick tick)
        {
        }

        public void Close(Tick tick)
        {
            tick.CloseNode(this);
            tick.BlackBoard.SetValue(false,IsOpenKey,tick.Tree.Id,Id);
            OnClose(tick);
        }

        public void Enter(Tick tick)
        {
            tick.EnterNode(this);
            OnEnter(tick);
        }

        public void Open(Tick tick)
        {
            tick.OpenNode(this);
            tick.BlackBoard.SetValue(true,IsOpenKey,tick.Tree.Id,Id);
            OnOpen(tick);
        }

        public BehaviorState Tick(Tick tick)
        {
            tick.TickNode(this);
            return OnTick(tick);
        }

        public void Exit(Tick tick)
        {
            tick.ExitNode(this);
            OnExit(tick);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            BaseNode other = obj as BaseNode;
            return other != null && Equals(other);
        }
        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        [DebuggerStepThrough]
        public static bool operator ==(BaseNode left, BaseNode right)
        {
            return Equals(left, right);
        }
        [DebuggerStepThrough]
        public static bool operator !=(BaseNode left, BaseNode right)
        {
            return !Equals(left, right);
        }
    }
}