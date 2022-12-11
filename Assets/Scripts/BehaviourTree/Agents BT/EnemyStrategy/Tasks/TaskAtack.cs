using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAtack : TreeNode
{
    GameManager _gameManager;    
       public TaskAtack(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public override TreeNodeState Evaluate()
    {
        int finalCard;
        List<int> posibleCardsIndex = new List<int>();
        object _target = GetData("target");
        Debug.Log("Estoy atacando a secas");
        if (_target != null)
        {           
            
            
            for (int i = 0; i < _gameManager.enemyCards.Count; i++)
            {
                if (_gameManager.enemyCards[i]._cardSetUp._cardCost < 2)
                {
                    posibleCardsIndex.Add(i);
                }
            }


            if (posibleCardsIndex.Count == 0)
            {
                finalCard = Random.Range(0, _gameManager.enemyCards.Count);
            }
            else if (posibleCardsIndex.Count == 1)
            {
                finalCard = posibleCardsIndex[0];
            }
            else
            {
                int randomNumber = Random.Range(0, posibleCardsIndex.Count);
                finalCard = posibleCardsIndex[randomNumber];
            }


            _gameManager.PlayCard(finalCard, GameObject.Find("SpawnSafe"));
            ClearData("target");
        }
        else
        {
            Debug.Log("Target es null.");
        }

        state = TreeNodeState.RUNNING;
        return state;
    }
}
