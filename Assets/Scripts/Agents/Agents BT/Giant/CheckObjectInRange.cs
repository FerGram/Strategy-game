using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckObjectInRange : TreeNode
{
    //Variables
    private int _objectsLayerMask = 1 << 6; //Hacer una layer con los castillos, se podria usar otra con los enemigos para otros agentes(?
    private Agent _agent;
    private float _rangeOfVision;

    //Constructor
    public CheckObjectInRange(Agent agent, float rangeOfVision)
    {
        _agent = agent;
        _rangeOfVision = rangeOfVision;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, _rangeOfVision, _objectsLayerMask);
            if (collision != null)
            {
                
                parent.parent.SetData("target", collision.transform);
                Debug.Log("detectado");
                state = TreeNodeState.SUCCESS;
                return state;
            }
            state = TreeNodeState.FAILURE;
            return state;

        }

        state = TreeNodeState.SUCCESS;
        return state;
    }

}
