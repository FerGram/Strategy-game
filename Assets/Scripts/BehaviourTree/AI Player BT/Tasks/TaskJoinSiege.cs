using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskJoinSiege : TreeNode
{
    //Esta tarea trata de acompañar a una tropa más fuerte, ya sea con el apoyo de tropas menos costosas
    //como lanzar una bola de fuego en la posición donde posiblemente vayan a aparecer una arqueras enemigas.
    
    private Card[] enemyCards;
    List<Card> posibleCards = new List<Card>(); 
    GameManager _gameManager;   
    public TaskJoinSiege(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        int finalCardIndex;
        List<int> posibleCardsIndex = new List<int>();
        int _target = (int)GetData("target");
      
        if (_target != null)
        {
            Debug.Log("Estoy uniendome al ataque.");
            for (int i = 0; i < _gameManager.enemyCards.Count; i++)
            {
                if (_gameManager.enemyCards[i]._cardCost < 2 && _gameManager.enemyCards[i]._cardType == CardSetUp.CARD_TYPE.ARCHER)
                {
                    posibleCardsIndex.Add(i);
                }
                else
                {
                    if(_gameManager.enemyTurnsMana == 2 && _gameManager.enemyCards[i]._cardType == CardSetUp.CARD_TYPE.BARBARIAN)
                        posibleCardsIndex.Add(i);
                }
            }

            if (posibleCardsIndex.Count == 0)
            {
                state = TreeNodeState.FAILURE;
                ClearData("target");
                return state;

                //finalCardIndex = Random.Range(0, _gameManager.enemyCards.Count);
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

            /*
            if(finalCard.type == "fireball"){
                posibleEnemyTroupesPosition[]
            }
            else{
                posibleAllyOffensiveSpawns[]
            }
            */

            _gameManager.PlayCard(finalCardIndex, _gameManager.enemyTowers[_target]);
            ClearData("target");

            

        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
