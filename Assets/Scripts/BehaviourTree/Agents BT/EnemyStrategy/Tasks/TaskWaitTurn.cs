using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWaitTurn : TreeNode
{
    //Variables
    GameManager _gameManager;
  

    //Constructor
    public TaskWaitTurn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        object _target = parent.GetData("target");
        if (_target != null)
        {
            Debug.Log("Estoy guardando turno.");
            _gameManager.StoreTurn();
            state = TreeNodeState.SUCCESS;            
            ClearData("target");
            return state;
        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
