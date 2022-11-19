using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] protected float _speed = 3f;
    [SerializeField] protected float _stoppingNodeDistance = 0.3f;

    [Header("Navigation")]
    [SerializeField] protected Pathfinder _pathfinder;


    protected void StartNavigation(Rigidbody2D rb, Transform to)
    {
        CancelNavigation();

        rb.velocity = Vector2.zero;
        List<Node> path = _pathfinder.GetPath(rb, to);

        if (path != null) StartCoroutine(NavigationRoutine(rb, path, _speed, _stoppingNodeDistance));
    }
    protected void StartNavigation(Rigidbody2D rb, Vector2 to)
    {
        CancelNavigation();

        rb.velocity = Vector2.zero;
        List<Node> path = _pathfinder.GetPath(rb, to);

        if (path != null) StartCoroutine(NavigationRoutine(rb, path, _speed, _stoppingNodeDistance));
    }

    IEnumerator NavigationRoutine(Rigidbody2D agent, List<Node> path, float speed, float stoppingNodeDistance)
    {
        /*
         *  NOTE: the rotation code is commented. At the time of writing, we won't rotate in 2D.
         */

        //Quaternion targetRotation = agent.transform.localRotation;
        while (path.Count > 0)
        {
            Vector2 agentPos = agent.position;
            Vector2 nodePos = path[0].GetPosition();

            while (Vector2.Distance(agent.position, path[0].GetPosition()) > stoppingNodeDistance)
            {
                //Move towards next node
                Vector2 movementDir = path[0].GetPosition() - agent.position;
                agent.velocity = movementDir.normalized * speed;

                yield return null;

                ////Finished rotation
                //if (Quaternion.Angle(agent.rotation, targetRotation) < 0.01f)
                //{
                //    Vector3 targetLook = (nodePos - agentPos);

                //    if (Mathf.Abs(targetLook.x) > Mathf.Abs(targetLook.z)) targetLook.z = 0;
                //    else targetLook.x = 0;

                //    targetLook = targetLook.normalized;

                //    targetRotation = Quaternion.LookRotation(targetLook, Vector3.up);
                //}
                //Quaternion desiredRotation = Quaternion.RotateTowards(agent.transform.localRotation, targetRotation, Mathf.PI * rotationSpeed);
                //agent.transform.localRotation = desiredRotation;
            }
            path.RemoveAt(0);
            yield return null;
        }
        agent.velocity = Vector2.zero;
    }

    protected void CancelNavigation()
    {
        StopCoroutine("StartNavigation");
    }
}
