using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAtacking : TreeNode
{
    //Variables   
    public static Agent[] allies;
    
    private int totalCostOfAllies;
    private GameManager _gameManager;

    public CheckAtacking(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            if (totalCostOfAllies > 1)
            {
                parent.parent.SetData("target", totalCostOfAllies); 
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
