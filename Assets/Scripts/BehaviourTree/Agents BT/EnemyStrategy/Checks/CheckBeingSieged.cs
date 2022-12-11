using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBeingSieged : TreeNode
{
    //Variables
    private GameManager _gameManager;
    private int[] listOfThreats;
    private int threatIndicator;
    
    public CheckBeingSieged(GameManager gameManager)
    {
        _gameManager = gameManager;
        threatIndicator = 1;
    }

    public override TreeNodeState Evaluate()
    {
        object t = parent.GetData("target");

        
        
        if (t == null && _gameManager.currentTurn == GameManager.TURN.ENEMY)
        {
            Debug.Log("Miro si me atacan.");
            listOfThreats = _gameManager.listOfThreats;
            int maximumThreat = 0;
            int towerIndexToDef = 0;

            for (int i = 0; i < listOfThreats.Length; i++)
            {
                Debug.Log("Maximum threat: " + maximumThreat.ToString());
                int currentThreat = listOfThreats[i];

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

            if(maximumThreat > threatIndicator)
            {
                Debug.Log("Amenaza: " + listOfThreats[towerIndexToDef]);
                parent.parent.SetData("target", towerIndexToDef); 
                state = TreeNodeState.SUCCESS;
                return state;
            }
            state = TreeNodeState.FAILURE;
            return state;



        }


        ClearData("target");
        state = TreeNodeState.FAILURE;
        return state;

    }
}
   

