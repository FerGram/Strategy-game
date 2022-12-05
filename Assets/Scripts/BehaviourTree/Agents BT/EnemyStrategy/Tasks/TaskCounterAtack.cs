using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCounterAtack : TreeNode
{
    private GameManager _gameManager;
    
    //La tarea consiste en defender si es necesario una torre con unidades pequeñas
    public TaskCounterAtack(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        int finalCardIndex;
        List<int> posibleCardsIndex = new List<int>();
        //Debug.Log(GetData("target").ToString());
        List<int> _target = (List<int>)GetData("target");
        if (_target != null)
        {
            
            for (int i = 0; i < _gameManager.enemyCards.Count; i++)
            {
                if(_gameManager.enemyCards[i]._cardSetUp._cardCost < 2)
                {
                    posibleCardsIndex.Add(i);
                }
            }
            

            if (posibleCardsIndex.Count == 0)
            {             
                finalCardIndex = Random.Range(0, _gameManager.enemyCards.Count);
            }
            else if (posibleCardsIndex.Count == 1)
            {
                finalCardIndex = posibleCardsIndex[0];
            }
            else
            {
                int randomNumber = Random.Range(0, posibleCardsIndex.Count);
                finalCardIndex = posibleCardsIndex[randomNumber];
            }

            _gameManager.PlayCard(finalCardIndex, _target[0]);
            Debug.Log("Estoy contraatacando.Estado: " + state.ToString());
            //Spawnea la carta en el sitio
            ClearData("target");
        }

        state = TreeNodeState.RUNNING;
        return state;
    }
}
