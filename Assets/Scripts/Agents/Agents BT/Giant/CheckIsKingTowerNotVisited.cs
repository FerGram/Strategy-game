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
    private LayerMask _LayerKingTower;

    //Constructor
    public CheckIsKingTowerNotVisited(Agent agent, float rangeOfVision, bool kingTowerNotVisited, LayerMask LayerKingTower)
    {
        _agent = agent;
        this.rangeOfVision = rangeOfVision;
        this.kingTowerNotVisited = kingTowerNotVisited;
        _LayerKingTower = LayerKingTower;
    }

    public override TreeNodeState Evaluate()
    {
        if (kingTowerNotVisited)
        {
            Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, rangeOfVision, _LayerKingTower);

            if (collision != null)
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

        state = TreeNodeState.FAILURE;
        return state;
    }

}