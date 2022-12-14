using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    //<summary>
    //Clase Secuencia: Act�a como un AND. Solo si todos los nodos tienen el
    //estado SUCCESS, �l tendra el estado SUCCESS. Deriva de la clase TreeNode
    //haciendo override de su m�todo Evaluate(). Si un nodo tiene el estado FAILURE,
    //dejar� de continuar y devolver� el estado.
    //<summary/>
    public class Sequence : TreeNode
    {
        public Sequence() : base() { }
        public Sequence(List<TreeNode> children) : base(children) { }
        public override TreeNodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(TreeNode node in children)
            {
                switch (node.Evaluate())
                {
                    case TreeNodeState.FAILURE:
                        state = TreeNodeState.FAILURE;
                        return state;
                    case TreeNodeState.SUCCESS:
                        continue;
                    case TreeNodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = TreeNodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? TreeNodeState.RUNNING : TreeNodeState.SUCCESS;
            return state;
        }
    }
}

