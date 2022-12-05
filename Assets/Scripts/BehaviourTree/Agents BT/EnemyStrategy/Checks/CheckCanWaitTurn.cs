using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanWaitTurn : TreeNode
{
    //Variables   
    
  
    private Agent _agent;
    private bool haveCost2 = false;    
    private List<int> cost2Index = new List<int>();

    GameManager _gameManager;
    

    public CheckCanWaitTurn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            if (_gameManager.enemyTurnsMana < 2)
            {
                for (int i = 0; i < _gameManager.enemyCards.Count; i++)
                {
                    if(_gameManager.enemyCards[i]._cardSetUp._cardCost == 2)
                    {
                        haveCost2 = true;
                        cost2Index.Add(i);
                    }
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
