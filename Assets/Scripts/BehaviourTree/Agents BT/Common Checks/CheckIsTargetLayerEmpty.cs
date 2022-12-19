using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIsTargetLayerEmpty : TreeNode
{
    //Variables
    private string _targetLayer;
    private GameObject[] objectsInTargetLayer;
    private Agent _agent;


    //Constructor
    public CheckIsTargetLayerEmpty(Agent agent, string targetLayer)
    {
        _targetLayer = targetLayer;
        _agent = agent;
    }

    public override TreeNodeState Evaluate()
    {
        //checkear la layer, si esta vacia, devolver false
        //Supone que tu ya le has pasado la layer de los agentes/edificios
        objectsInTargetLayer = FindGameObjectsInLayer(_targetLayer);

        if (objectsInTargetLayer == null)
        {
            return TreeNodeState.FAILURE;
        }
        else
        {
            return TreeNodeState.SUCCESS;
        }
    }
    private GameObject[] FindGameObjectsInLayer(string layer)
    {
        GameObject[] objectsInScene = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
        List<GameObject> objectsInLayer = new List<GameObject>();

        for (int i = 0; i < objectsInScene.Length; i++)
        {
            if ((objectsInScene[i].layer == LayerMask.NameToLayer(layer)) && objectsInScene[i] != _agent.gameObject) objectsInLayer.Add(objectsInScene[i]);
        }

        if (objectsInLayer.Count == 0) return null;

        return objectsInLayer.ToArray();
    }
}
