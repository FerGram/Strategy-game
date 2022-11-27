using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsSmallTowerNotVisited : TreeNode
{
    //Variables
    private Agent _agent;
    public static int _objectsLayerMask = 1 << 6; //Hacer una layer con los castillos, se podria usar otra con los enemigos para otros agentes(?
    private bool smallTowerNotVisited;
    private float rangeOfVision;

    //Constructor
    public CheckIsSmallTowerNotVisited(Agent agent, float rangeOfVision, bool smallTowerNotVisited)
    {
        _agent = agent;
        this.smallTowerNotVisited = smallTowerNotVisited;
        this.rangeOfVision = rangeOfVision;
    }

    public override TreeNodeState Evaluate()
    {
        /*GameObject t = (GameObject)GetData("target");
        if (t != null)
        {
            Tower tower = t.GetComponent<Tower>();
            if (tower.life <= 0)
            {
                //parent.parent.SetData("target", GameObject.FindGameObjectWithTag("KingTower"));
                state = TreeNodeState.SUCCESS;
                return state;
            }
            state = TreeNodeState.FAILURE;
            return state;

        }

        state = TreeNodeState.SUCCESS;
        return state;*/

        if (smallTowerNotVisited)
        {
            object t = GetData("target");
            if (t == null)
            {
                Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, rangeOfVision, _objectsLayerMask);
                if (collision != null)
                {

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
