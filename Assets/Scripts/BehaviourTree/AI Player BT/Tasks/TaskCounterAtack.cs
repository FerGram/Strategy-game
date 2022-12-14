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
        Debug.Log("Estoy contraatacando.Estado: " + state.ToString());
        object _target = GetData("target");
        Debug.Log("Target: " +_target);
        if (_target != null)
        {

            int maximumThreat = 0;
            int towerIndexToDef = 0;
            for (int i = 0; i < _gameManager.listOfThreats.Length; i++)
            {
                int currentThreat = _gameManager.listOfThreats[i];

                //Si es la torre grande la amenaza se multiplica x2
                if (i == 2)
                {
                    currentThreat *= 2;
                }

                if (currentThreat > maximumThreat)
                {
                    maximumThreat = currentThreat;
                    towerIndexToDef = i;
                }

            }

            for (int i = 0; i < _gameManager.enemyCards.Count; i++)
            {
                if(_gameManager.enemyCards[i]._cardCost < 2)
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

            _gameManager.PlayCard(finalCardIndex, towerIndexToDef);
            
            Debug.Log("Torre a defender: " + towerIndexToDef.ToString());
            //Spawnea la carta en el sitio
            ClearData("target");
        }

        state = TreeNodeState.RUNNING;
        return state;
    }
}
