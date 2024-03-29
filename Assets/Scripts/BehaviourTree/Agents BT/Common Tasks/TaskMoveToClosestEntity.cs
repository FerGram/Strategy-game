using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveToClosestEntity : TreeNode
{
    //Variables
    private Agent _agent;
    private int _objectsLayerMask;

    private Vector2 _infinityVector = new Vector2(1000000, 1000000);
    private Vector2 _currentTarget;

    //Constructor
    public TaskMoveToClosestEntity(Agent agent, LayerMask targetLayer)
    {
        _agent = agent;
        _objectsLayerMask = targetLayer;
        _currentTarget = _infinityVector;
    }

    public override TreeNodeState Evaluate()
    {
        if (_currentTarget != _infinityVector)
        {
            if (_agent.IsNavigatingTowards(_currentTarget)) return TreeNodeState.RUNNING;
            else
            {
                _currentTarget = _infinityVector;
                return TreeNodeState.SUCCESS;
            }
        }

        EntityHealth[] damageableEntities = GameObject.FindObjectsOfType<EntityHealth>();
        
        
        if (damageableEntities.Length > 0)
        {
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
                if (distanceToEntity < minDistance)
                {
                    minDistance = distanceToEntity;
                    minDistanceEntity = damageableEntities[i];
                }
            }

            if (minDistanceEntity != null)
            {
                Debug.Log("Navigating towards new target...");
                
                if (minDistanceEntity.gameObject.layer == LayerMask.NameToLayer("Buildings"))
                {
                    _currentTarget = minDistanceEntity.transform.GetChild(0).position;
                }
                else
                {
                    _currentTarget = minDistanceEntity.transform.position;
                }


                FaceTarget(minDistanceEntity);

                _agent.targetTower = minDistanceEntity.gameObject;
                _agent.StartNavigation(_currentTarget);

                return TreeNodeState.RUNNING;
            }
        }
        _currentTarget = _infinityVector;
        return TreeNodeState.FAILURE;




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
