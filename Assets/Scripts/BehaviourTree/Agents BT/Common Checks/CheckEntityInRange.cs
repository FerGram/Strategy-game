using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckEntityInRange : TreeNode
{
    //Variables
    private Agent _agent;
    private float _attackRange;
    private int _objectsLayerMask;

    private EntityHealth _currentTarget;

    //Constructor
    public CheckEntityInRange(Agent agent, float rangeOfVision, LayerMask targetLayer)
    {
        _agent = agent;
        _attackRange = rangeOfVision;
        _objectsLayerMask = targetLayer;
    }

    public override TreeNodeState Evaluate()
    {
        EntityHealth target = (EntityHealth) parent.GetData("Target");

        if(target == null)
        {
            EntityHealth[] damageableEntities = GameObject.FindObjectsOfType<EntityHealth>();

            if (damageableEntities.Length > 0)
            {
                //Find the closest entity within attackRange

                Vector2 agentPos = _agent.transform.position;
                Vector2 entityPos;

                float minDistance = Mathf.Infinity;
                EntityHealth minDistanceEntity = null;

                for (int i = 0; i < damageableEntities.Length; i++)
                {
                    //Continue if does not belong to any target layer
                    if ((1 << damageableEntities[i].gameObject.layer & _objectsLayerMask) == 0) continue;
                    if (damageableEntities[i].gameObject == _agent.gameObject) continue;
                    if (damageableEntities[i].gameObject.CompareTag(_agent.gameObject.tag)) continue;

                    entityPos = damageableEntities[i].transform.position;
                    float distanceToEntity = Vector2.Distance(agentPos, entityPos);
                    if (distanceToEntity < minDistance && distanceToEntity < _attackRange)
                    {
                        minDistance = distanceToEntity;
                        minDistanceEntity = damageableEntities[i];
                    }
                }

                if (minDistanceEntity != null)
                {
                    Debug.Log("Target in range set");

                    parent.SetData("Target", minDistanceEntity);
                    _currentTarget = minDistanceEntity;

                    return TreeNodeState.SUCCESS;
                }
            }
            return TreeNodeState.FAILURE;
        }
        else if (target == _currentTarget && Vector2.Distance(_agent.transform.position, target.transform.position) <= _attackRange)
        {
            return TreeNodeState.SUCCESS;
        }

        _currentTarget = null;
        parent.ClearData("Target");
        return TreeNodeState.FAILURE;
    }

}
