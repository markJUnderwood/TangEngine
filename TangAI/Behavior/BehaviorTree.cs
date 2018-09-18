using System;
using System.Collections.Generic;
using TangAI.Behavior.Nodes;

namespace TangAI.Behavior
{
    public class BehaviorTree
    {
        public BehaviorTree(BaseNode rootNode) : this(rootNode, Guid.NewGuid().ToString("N"))
        {
        }

        public BehaviorTree(BaseNode rootNode, string id)
        {
            Root = rootNode;
            Id = id;
        }

        public string Id { get; }

        public BaseNode Root { get; }

        internal void Tick(object target, Blackboard blackboard)
        {
            if (blackboard == null)
                throw new ArgumentNullException(nameof(blackboard));
            Tick tick = new Tick {Target = target, Tree = this, BlackBoard = blackboard};

            Root.Execute(tick);
            List<BaseNode> nodes = blackboard.GetTreeScope(Id).Nodes;
            int start = 0;
            int limit = Math.Min(tick._nodes.Count, nodes.Count);
            for (int i = 0; i < limit; i++, start = i + 1)
                if (nodes[i] != tick._nodes[i])
                    break;

            for (int i = nodes.Count - 1; i >= start; i--)
                nodes[i].Close(tick);

            blackboard.GetTreeScope(Id).Nodes = tick._nodes;
        }
    }
}