using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        protected List<Node> nodes = new List<Node>();
        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            bool isAnyNodeRun = false;

            foreach (var n in nodes)
            {
                switch (n.Evaluate())
                {
                    case NodeState.RUNNING:
                        isAnyNodeRun = true;
                        break;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.FAILURE:
                        nodeState = NodeState.FAILURE;
                        return nodeState;
                }
            }

            nodeState = isAnyNodeRun ? NodeState.RUNNING : NodeState.SUCCESS;
            return nodeState;
        }
    }
}