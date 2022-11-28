using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskJoinSiege : TreeNode
{
    //Esta tarea trata de acompañar a una tropa más fuerte, ya sea con el apoyo de tropas menos costosas
    //como lanzar una bola de fuego en la posición donde posiblemente vayan a aparecer una arqueras enemigas.
    Card finalCard;
    private Card[] playerCards;
    List<Card> posibleCards = new List<Card>();
    GameObject[] posibleEnemyTroupesPositions = new GameObject[2];
    private Agent _agent;
    GameObject[] posibleAllyOffensiveSpawns = new GameObject[2];

    public TaskJoinSiege(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        Transform _target = (Transform)GetData("target");
        if (_target != null)
        {
            /*
            * Falta implementar el coste de las cartas
            for (int i = 0; i < playerCards.Length; i++)
            {
                if(playerCards[i].cost == 1)
                {
                    posibleCards.Add(playerCards[i]);
                }
            }
            */

            if (posibleCards.Count == 0)
            {
                int randomNumber = Random.Range(0, posibleCards.Count);
                finalCard = playerCards[randomNumber];
            }
            else if (posibleCards.Count == 1)
            {
                finalCard = posibleCards[0];
            }
            else
            {
                int randomNumber = Random.Range(0, posibleCards.Count);
                finalCard = posibleCards[randomNumber];
            }
            /*
            if(finalCard.type == "fireball"){
                posibleEnemyTroupesPosition[]
            }
            else{
                posibleAllyOffensiveSpawns[]
            }
            */
            
            ClearData("target");

        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
