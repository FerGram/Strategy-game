using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

//El agente patrullará hacia un punto determinado del mapa
public class TaskMoveToClosestTower : TreeNode
{
    //Variables
    private Agent agent; 
    private Animator animator;
    private GameObject[] smallTowers;

    //Constructor
    public TaskMoveToClosestTower(Agent agent, GameObject[] towers, Animator anim)
    {
        this.agent = agent;
        smallTowers = towers;
        animator = anim;
    }

    public override TreeNodeState Evaluate()
    {
        Transform _target = (Transform)GetData("target");
        if (_target == null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
            if (Vector2.Distance(agent.gameObject.transform.position, smallTowers[0].gameObject.transform.position) <
                Vector2.Distance(agent.gameObject.transform.position, smallTowers[1].gameObject.transform.position))
            {
                if (!agent.IsNavigatingTowards(smallTowers[0].transform.GetChild(0).position))
                    agent.StartNavigation(smallTowers[0].transform.GetChild(0)); //Devuelve el transform del hijo, que es el target al que tiene que moverse
            }
            else
            {
                if (!agent.IsNavigatingTowards(smallTowers[1].transform.GetChild(0).position)) 
                    agent.StartNavigation(smallTowers[1].transform.GetChild(0));
            }

        }
        state = TreeNodeState.RUNNING;
        return state;
        
    }
}
