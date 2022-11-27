using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveToKingTower : TreeNode
{
    //Variables
    private Agent agent;
    private Animator animator;
    private GameObject kingTower;

    //Constructor
    public TaskMoveToKingTower(Agent agent, GameObject kingTower, Animator anim)
    {
        this.agent = agent;
        this.kingTower = kingTower;
        animator = anim;
    }

    public override TreeNodeState Evaluate()
    {

        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);

        if (!agent.IsNavigatingTowards(kingTower.transform.GetChild(0).position))
            agent.StartNavigation(kingTower.transform.GetChild(0));

        state = TreeNodeState.RUNNING;
        return state;

    }
}