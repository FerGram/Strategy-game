using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

//El agente patrullará hacia un punto determinado del mapa
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
        //Escribir aquí el código para la task
        //...
        state = TreeNodeState.RUNNING;
        return state;
        
    }
}
