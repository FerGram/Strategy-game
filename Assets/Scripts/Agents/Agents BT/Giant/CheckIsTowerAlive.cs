using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsTowerAlive : TreeNode
{
    //Constructor
    public CheckIsTowerAlive()
    {   }

    public override TreeNodeState Evaluate()
    {
        Transform t = (Transform)GetData("target");
        if (t != null)
        {
            EntityHealth tower = t.gameObject.GetComponent<EntityHealth>();
            
            if (tower.GetHealth() > 0)
            {
                state = TreeNodeState.SUCCESS;
                return state;               
            }
            state = TreeNodeState.FAILURE;
            return state;

        }

        state = TreeNodeState.FAILURE;
        return state;
    }
}
