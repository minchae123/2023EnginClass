using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Inverter : Node
    {
        protected Node node;
        public  Inverter(Node node)
        {
            this.node = node;
        }

        public override NodeState Evaluate()
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    nodeState = NodeState.SUCCESS;
                    break;
                case NodeState.FAILURE:
                    nodeState = NodeState.FAILURE;
                    break;
            }
            return nodeState;
        }
    }
}