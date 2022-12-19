using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourTree;

//El agente atacará el objetivo
public class TaskAttackEntity : TreeNode
{
    //Variables
    private Agent _agent;
    private float attackDamage;
    private float timeBetweenAttacks;
    private Animator animator;

    private float attackTime = 0;

    //Constructor
    public TaskAttackEntity(Agent agent, float dmg, float timeBtwAttacks, Animator anim)
    {
        this._agent = agent;
        attackDamage = dmg;
        animator = anim;
        timeBetweenAttacks = timeBtwAttacks;
    }

    public override TreeNodeState Evaluate()
    {
        EntityHealth target = (EntityHealth) parent.GetData("Target");

        if (target != null && target.GetHealth() > 0)
        {
            FaceTarget(target);
            _agent.CancelNavigation();

            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsWalking", false);

            attackTime += Time.deltaTime;
            if (attackTime >= timeBetweenAttacks)
            {
                target.TakeDamage(attackDamage);
                attackTime = 0f;
                if (target.GetHealth() <= 0)
                {
                    parent.ClearData("Target");
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsWalking", true);
                    Debug.Log("He matado a una entidad del tipo " + target.gameObject.layer);
                    return TreeNodeState.SUCCESS;
                }
            }
            return TreeNodeState.RUNNING;
        }
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", true);
        parent.ClearData("Target");
        return TreeNodeState.SUCCESS;
    }

    private void FaceTarget(EntityHealth target)
    {
        if (Mathf.Abs(target.transform.position.x) - Mathf.Abs(_agent.transform.position.x) >= 0)
        {
            Vector3 scale = _agent.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            _agent.transform.localScale = scale;

        }
        else
        {
            Vector3 scale = _agent.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            _agent.transform.localScale = scale;
        }
    }
}