using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAtacking : TreeNode
{
    //Variables   
    public static Agent[] allies;
    
    private int totalCostOfAllies;
    private GameManager _gameManager;

    public CheckAtacking(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override TreeNodeState Evaluate()
    {
        object t = parent.GetData("target");

        if (t == null && _gameManager.currentTurn == GameManager.TURN.ENEMY)
        {
            //_gameManager.iaStatsText.text += "\nComprobando si estoy atacando...";
            GameObject[] agentList = FindGameObjectsInLayer("Agents");
            
            if (agentList != null)
            {
                int[] eachTowerThreat = new int[3];
                foreach (var agentGameobject in agentList)
                {
                    if (agentGameobject.CompareTag("Enemy")){
                        for (int tower = 0; tower < _gameManager.allyTowers.Length; tower++)
                        {
                            if(agentGameobject.GetComponent<Agent>().targetTower == _gameManager.allyTowers[tower])
                            {
                                eachTowerThreat[tower] += agentGameobject.GetComponent<Agent>().cost;
                            }
                        }
                    }
                }
                int maximumThreat = 0;
                int towerIndex = 0;
                for (int threat = 0; threat < eachTowerThreat.Length; threat++)
                {
                 

                    if(eachTowerThreat[threat] > maximumThreat)
                    {
                        maximumThreat = eachTowerThreat[threat];
                        towerIndex = threat;
                    }
                }

                if (maximumThreat >= 2)
                {
                    parent.parent.SetData("target", towerIndex);
                    state = TreeNodeState.SUCCESS;
                    return state;
                }
                else
                {
                    state = TreeNodeState.FAILURE;
                    return state;
                }
                
            }
            
            
        }

        state = TreeNodeState.FAILURE;
        ClearData("target");
        return state;

    }

    private GameObject[] FindGameObjectsInLayer(string layer)
    {
        GameObject[] objectsInScene = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
        List<GameObject> objectsInLayer = new List<GameObject>();

        for (int i = 0; i < objectsInScene.Length; i++)
        {
            if (objectsInScene[i].layer == LayerMask.NameToLayer(layer)) objectsInLayer.Add(objectsInScene[i]);
        }

        if (objectsInLayer.Count == 0) return null;

        return objectsInLayer.ToArray();
    }
}
