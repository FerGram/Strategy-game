using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourTree;

//El agente atacará el objetivo
public class TaskAttackTarget : TreeNode
{
    //Variables
    private float damage;
    private float attackTime;
    private Animator animator;

    private EntityHealth tower;
    private float attackCounter = 0;

    //Constructor
    public TaskAttackTarget(float dmg, float attackTime, Animator anim)
    {
        damage = dmg;
        animator = anim;
        this.attackTime = attackTime;
    }

    public override TreeNodeState Evaluate()
    {
        Transform _target = (Transform)GetData("target");
        if (_target != null)
        {
            tower = _target.GetComponentInChildren<EntityHealth>();
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsWalking", false);
           
            attackCounter += Time.deltaTime;
            if(attackCounter >= attackTime)
            {
                tower.TakeDamage(damage);
                attackCounter = 0f;

            }

            if(tower.GetHealth() <= 0)
            {
                ClearData("target");
                //parent.parent.SetData("target", collision.transform);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsWalking", true);

            }
            
        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}