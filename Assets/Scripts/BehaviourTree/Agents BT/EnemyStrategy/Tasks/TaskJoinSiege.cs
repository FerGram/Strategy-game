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
        object _target = GetData("target");
        Debug.Log("Estoy uniendome al ataque.");
        if (_target != null)
        {
            for (int i = 0; i < enemyCards.Length; i++)
            {
                if (enemyCards[i]._cardSetUp._cardCost < 2)
                {
                    posibleCardsIndex.Add(i);
                }
            }

            if (posibleCardsIndex.Count == 0)
            {
                finalCardIndex = Random.Range(0, enemyCards.Length);
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

            _gameManager.PlayCard(finalCardIndex, _gameManager.enemyTowers[0]);
            ClearData("target");

        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
