using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveToTarget : TreeNode
{
    //Variables
    private Agent agent;
    private Animator animator;
    private GameObject target;

    //Constructor
    public TaskMoveToTarget(Agent agent, GameObject target, Animator anim)
    {
        this.agent = agent;
        this.target = target;
        animator = anim;
    }

    public override TreeNodeState Evaluate()
    {

        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);

        if (!agent.IsNavigatingTowards(target.transform.GetChild(0).position))
            agent.StartNavigation(target.transform.GetChild(0));

        state = TreeNodeState.RUNNING;
        return state;

    }
}