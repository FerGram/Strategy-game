using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsSmallTowerNotVisited : TreeNode
{
    //Variables
    private Agent _agent;
    private LayerMask _objectsLayerMask;
    private bool smallTowerNotVisited;
    private float rangeOfVision;

    //Constructor
    public CheckIsSmallTowerNotVisited(Agent agent, float rangeOfVision, bool smallTowerNotVisited, LayerMask objectsLayerMask)
    {
        _agent = agent;
        this.smallTowerNotVisited = smallTowerNotVisited;
        this.rangeOfVision = rangeOfVision;
        _objectsLayerMask = objectsLayerMask;
    }

    public override TreeNodeState Evaluate()
    {
        if (smallTowerNotVisited)
        {
            object t = GetData("target");
            if (t == null)
            {
                Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, rangeOfVision, _objectsLayerMask);
                if (collision != null)
                {
                    _agent.CancelNavigation();
                    parent.parent.SetData("target", collision.transform);
                    Debug.Log("detectado");
                    smallTowerNotVisited = false; //torre pequeña visitada
                    state = TreeNodeState.FAILURE;
                    return state;
                }
                state = TreeNodeState. SUCCESS;
                return state;

            }
        }

        state = TreeNodeState.FAILURE;
        return state;
    }

}
