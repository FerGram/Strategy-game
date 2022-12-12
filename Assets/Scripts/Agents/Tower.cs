using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    enum TowerType { TowerSmall, TowerKing }

    public int threatLevel;
    [SerializeField] float defendingRadius = 1;

    [SerializeField] TowerType _towerType;
    [SerializeField] private float attackDamage = 1;
    [SerializeField] private float timeBetween = 3;
    private float attackTime;
    private bool isShooting;
    private GameObject target;
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

    private void Update()
    {
        //Ataque de torres
        if(target == null)
        {
            GameObject[] agentList = FindGameObjectsInLayer("Agents");

            if (agentList != null)
            {
                foreach (var agentGameobject in agentList)
                {
                    if (gameObject.tag != agentGameobject.tag && Vector3.Distance(agentGameobject.transform.position, transform.position) < defendingRadius)
                    {
                        target = agentGameobject;
                    }
                }
            }
            

           
        }
        else
        {
            attackTime += Time.deltaTime;
            if (attackTime >= timeBetween)
            {
                target.GetComponent<EntityHealth>().TakeDamage(attackDamage);
                attackTime = 0f;

            }
        }
       
    }

    public void RecalculateThreat()
    {
        //print("Recalcular amenaza.");
        GameObject[] agentList = FindGameObjectsInLayer("Agents");
        int temporalThreat = 0;

        if(agentList != null)
        {
            foreach (var agentGameobject in agentList)
            {
                if (agentGameobject.CompareTag("Player") && agentGameobject.GetComponent<Agent>().targetTower == gameObject)
                {

                    temporalThreat += agentGameobject.GetComponent<Agent>().cost;
                }
                else
                {
                    if (Vector2.Distance(transform.position, agentGameobject.transform.position) <= defendingRadius)
                        temporalThreat -= agentGameobject.GetComponent<Agent>().cost;
                }
            }

            threatLevel = temporalThreat;
        }
        
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

    void OnDrawGizmos()
    {
        Handles.Label(transform.position, threatLevel.ToString());
    }
}


