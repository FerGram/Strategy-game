using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourTree;

//El agente patrullará hacia un punto determinado del mapa
public class TaskAttackTarget : TreeNode
{
    //Variables
    private Agent _agent;
    private Tower tower;
    private float _attackCounter = 0;

    //Constructor
    public TaskAttackTarget(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        Transform _target = (Transform)GetData("target");
        if (_target != null)
        {

            tower = _target.GetComponentInChildren<Tower>();
            GigantBT.animator.SetBool("IsAttacking", true);
            GigantBT.animator.SetBool("IsWalking", false);
           
            _attackCounter += Time.deltaTime;
            if(_attackCounter >= GigantBT.attackTime)
            {
                tower.TakeHit(GigantBT.damage);
                _attackCounter = 0f;

            }

            if(tower.life <= 0)
            {
                ClearData("target");
                //parent.parent.SetData("target", collision.transform);
                GigantBT.animator.SetBool("IsAttacking", false);
                GigantBT.animator.SetBool("IsWalking", true);

            }
            
        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}