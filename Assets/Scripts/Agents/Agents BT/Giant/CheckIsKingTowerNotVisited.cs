using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsKingTowerNotVisited : TreeNode
{
    //Variables
    private Agent _agent;
    public static int _objectsLayerMask = 1 << 6;
    private float rangeOfVision;
    private bool kingTowerNotVisited;

    //Constructor
    public CheckIsKingTowerNotVisited(Agent agent, float rangeOfVision, bool kingTowerNotVisited)
    {
        _agent = agent;
        this.rangeOfVision = rangeOfVision;
        this.kingTowerNotVisited = kingTowerNotVisited;
    }

    public override TreeNodeState Evaluate()
    {
        if (kingTowerNotVisited)
        {
            Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, rangeOfVision, _objectsLayerMask);
            if (collision != null)
            {
                if (collision.gameObject.tag == "KingTower")
                {
                    parent.parent.SetData("target", collision.transform);
                    Debug.Log("torre grande detectada");
                    kingTowerNotVisited = false;
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