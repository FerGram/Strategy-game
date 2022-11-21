using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTree
{
    //<summary>
    //Clase Selector: Actúa como un OR. Si un nodo tiene el estado
    //SUCCESS o RUNNING, será SUCCESS (o RUNNING). Si uno falla, se continuará evaluando.
    //Si todos fallan, será FAILURE.
    //<summary/>
    public class Selector : TreeNode
    {
        public override TreeNodeState Evaluate()
        {
            foreach (TreeNode node in children)
            {
                switch (node.Evaluate())
                {
                    case TreeNodeState.FAILURE:
                        continue;
                    case TreeNodeState.SUCCESS:
                        state = TreeNodeState.SUCCESS;
                        return state;
                    case TreeNodeState.RUNNING:
                        state = TreeNodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = TreeNodeState.FAILURE;
            return state;
        }
    }
}