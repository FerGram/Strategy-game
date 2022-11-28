using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanWaitTurn : TreeNode
{
    //Variables   
    public static Card[] currentCards;
    public static int turnsStored;
    private Agent _agent;
    private bool haveCost2 = false;    
    private List<int> cost2Index = new List<int>();
    

    public CheckCanWaitTurn(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            if (turnsStored == 1)
            {
                for (int i = 0; i < currentCards.Length; i++)
                {
                    /*
                    if (item.cost > 1)
                    {
                        haveCost2 = true;
                        cost2Index.Add(i);
                    }
                    */
                }

                if (haveCost2)
                {
                    parent.parent.SetData("target", cost2Index);
                    state = TreeNodeState.SUCCESS;
                    return state;
                }
                state = TreeNodeState.FAILURE;
                return state;
            }
            
        }

        state = TreeNodeState.SUCCESS;
        return state;

    }
}
