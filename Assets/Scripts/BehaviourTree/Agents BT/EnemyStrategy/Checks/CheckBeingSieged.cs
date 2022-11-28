using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBeingSieged : TreeNode
{
    //Variables
    public static Card[] currentCards;
    public static Agent[] enemyUnits;
    private Agent _agent;
    private int totalCostOfEnemies;
    
    public CheckBeingSieged(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");

        if(t == null)
        {
            if(totalCostOfEnemies > 1)
            {
                parent.parent.SetData("target", totalCostOfEnemies); //Preguntar a maría
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
   

