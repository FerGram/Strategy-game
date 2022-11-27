using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckObjectInRange : TreeNode
{
    //Variables
    public static int _objectsLayerMask = 1 << 6; //Hacer una layer con los castillos, se podria usar otra con los enemigos para otros agentes(?
    private Agent _agent;

    //Constructor
    public CheckObjectInRange(Agent agent)
    {
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        object t = GetData("target");
        if(t == null)
        {
            Collider2D collision = Physics2D.OverlapCircle(_agent.gameObject.transform.position, GigantBT.rangeOfVision, _objectsLayerMask);
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
