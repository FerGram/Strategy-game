using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveToKingTower : TreeNode
{
    //Variables
    private Agent _agent;

    //Constructor
    public TaskMoveToKingTower(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {

            GigantBT.animator.SetBool("IsWalking", true);
            GigantBT.animator.SetBool("IsAttacking", false);
            
            _agent.StartNavigation(_agent.rb, GigantBT.kingTower.transform.GetChild(0)); 

        state = TreeNodeState.RUNNING;
        return state;

    }
}