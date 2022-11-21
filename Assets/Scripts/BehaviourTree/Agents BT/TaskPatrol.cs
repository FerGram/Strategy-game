using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

//El agente patrullar� hacia un punto determinado del mapa
public class TaskPatrol : TreeNode
{
    //Variables
    private Transform _transform;
    private Transform _target;
    //...

    //Constructor
    public TaskPatrol(Transform transform, Transform target)
    {
        _transform = transform;
        _target = target;
        //...
    }

    public override TreeNodeState Evaluate()
    {
        //Escribir aqu� el c�digo para la task
        //...
        state = TreeNodeState.RUNNING;
        return state;
        
    }
}
