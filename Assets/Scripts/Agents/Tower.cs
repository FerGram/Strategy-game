using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    enum TowerType { TowerSmall, TowerKing }

    public int threatLevel;
    [SerializeField] float defendingRadius;

    [SerializeField] TowerType _towerType;

    private void OnDestroy()
    {
        switch (_towerType)
        {
            case TowerType.TowerSmall:

                //Give point
                break;

            case TowerType.TowerKing:

                //GameOver
                break;
        }
            
    }

    public void RecalculateThreat()
    {
        GameObject[] agentList = FindGameObjectsInLayer("Agents");
        int temporalThreat = 0;
        foreach (var agentGameobject in agentList)
        {           
            if (agentGameobject.CompareTag("Ally")){
                temporalThreat += agentGameobject.GetComponent<Agent>().cost;
            }
            else
            {
                if(Vector2.Distance(transform.position, agentGameobject.transform.position) <= defendingRadius)
                    temporalThreat -= agentGameobject.GetComponent<Agent>().cost;
            }
        }

        threatLevel = temporalThreat;
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


