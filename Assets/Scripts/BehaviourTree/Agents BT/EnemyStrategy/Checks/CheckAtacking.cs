using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAtacking : TreeNode
{
    //Variables   
    public static Agent[] allies;
    private Agent _agent;
    private int totalCostOfAllies;

    public CheckAtacking(Agent agent)
    {
        _agent = agent;
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

        state = TreeNodeState.SUCCESS;
        return state;

    }
}
