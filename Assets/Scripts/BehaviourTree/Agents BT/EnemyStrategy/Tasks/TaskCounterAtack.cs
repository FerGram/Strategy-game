using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCounterAtack : TreeNode
{
    private Agent _agent;
    private Card[] playerCards;
    private GameObject agentSpawn;
    //La tarea consiste en defender si es necesario una torre con unidades pequeñas
    public TaskCounterAtack(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        Card finalCard;
        List<Card> posibleCards = new List<Card>();
        Transform _target = (Transform)GetData("target");
        if (_target != null)
        {
            /*
             * Falta implementar el coste de las cartas
            for (int i = 0; i < playerCards.Length; i++)
            {
                if(playerCards[i].cost == 2)
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

            //Spawnea la carta en el sitio
            ClearData("target");
        }

        state = TreeNodeState.RUNNING;
        return state;
    }
}
