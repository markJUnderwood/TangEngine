using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangAI.Behavior.Nodes
{
    public class StateReturnNode:BaseNode
    {
        private readonly BehaviorState _returnState;

        public StateReturnNode(BehaviorState returnState=BehaviorState.Success):this(Guid.NewGuid().ToString("N"),returnState)
        {
        }

        public StateReturnNode(string id, BehaviorState returnState = BehaviorState.Success) : base(id)
        {
            _returnState = returnState;
        }

        protected override BehaviorState OnTick(Tick tick)
        {
            return _returnState;
        }
    }
}
