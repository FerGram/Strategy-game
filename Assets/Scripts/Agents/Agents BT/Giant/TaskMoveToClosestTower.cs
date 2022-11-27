using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

//El agente patrullará hacia un punto determinado del mapa
public class TaskMoveToClosestTower : TreeNode
{
    //Variables
    private Agent _agent; 

    //Constructor
    public TaskMoveToClosestTower(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {

            GigantBT.animator.SetBool("IsWalking", true);
            GigantBT.animator.SetBool("IsAttacking", false);
            if (Vector2.Distance(_agent.gameObject.transform.position, GigantBT.smallTowers[0].gameObject.transform.position) <
                Vector2.Distance(_agent.gameObject.transform.position, GigantBT.smallTowers[1].gameObject.transform.position))
            {
                _agent.StartNavigation(GigantBT.smallTowers[0].transform.GetChild(0)); //Devuelve el transform del hijo, que es el target al que tiene que moverse
            }
            else
            {
                _agent.StartNavigation(GigantBT.smallTowers[1].transform.GetChild(0));
            }

        
        state = TreeNodeState.RUNNING;
        return state;
        
    }
}
