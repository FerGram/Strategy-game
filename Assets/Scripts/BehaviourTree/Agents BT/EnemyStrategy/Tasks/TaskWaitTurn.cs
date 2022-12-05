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
        List<int> _target = (List<int>)GetData("target");
        if (_target == null)
        {
            _gameManager.StoreTurn();
            ClearData("target");
        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
