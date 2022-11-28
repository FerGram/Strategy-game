using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskWaitTurn : TreeNode
{
    //Variables
    private Agent _agent;
    public static int turnStorer;

    //Constructor
    public TaskWaitTurn(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        Transform _target = (Transform)GetData("target");
        if (_target == null)
        {
            turnStorer++;           
        }

        state = TreeNodeState.RUNNING;
        return state;

    }
}
