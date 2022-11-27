using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsKingTowerNotVisited : TreeNode
{
    //Variables
    private Agent _agent;
    public static int _objectsLayerMask = 1 << 6;

    //Constructor
    public CheckIsKingTowerNotVisited(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        if (GigantBT.kingTowerNotVisited)
        {
            Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, GigantBT.rangeOfVision, _objectsLayerMask);
            if (collision != null)
            {
                if (collision.gameObject.tag == "KingTower")
                {
                    parent.parent.SetData("target", collision.transform);
                    Debug.Log("torre grande detectada");
                    GigantBT.kingTowerNotVisited = false;
                    state = TreeNodeState.FAILURE;
                    return state;
                }
                state = TreeNodeState.SUCCESS;
                return state;
            }
            state = TreeNodeState.SUCCESS;
            return state;
        }

        state = TreeNodeState.FAILURE;
        return state;
    }

}