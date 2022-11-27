using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] protected float _speed = 3f;
    [SerializeField] protected float _stoppingNodeDistance = 0.3f;

    [Header("Navigation")]
    [SerializeField] protected Pathfinder _pathfinder;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(gameObject.transform.position, GigantBT.rangeOfVision);
    }

    public void StartNavigation(Rigidbody2D rb, Transform to)
    {
        StartNavigation(rb, new Vector2(to.position.x, to.position.y));
    }
    public void StartNavigation(Rigidbody2D rb, Vector2 to)
    {
        CancelNavigation();

        rb.velocity = Vector2.zero;

        StartCoroutine(NavigationRoutine(rb, to, _speed, _stoppingNodeDistance));
    }

    IEnumerator NavigationRoutine(Rigidbody2D agent, Vector2 to, float speed, float stoppingNodeDistance)
    {
        List<Node> path = _pathfinder.GetPath(agent, to);

        if (path != null)
        {
            while (path.Count > 0)
            {
                while (Vector2.Distance(agent.position, path[0].GetPosition()) > stoppingNodeDistance)
                {
                    //Move towards next node
                    Vector2 movementDir = path[0].GetPosition() - agent.position;
                    agent.velocity = movementDir.normalized * speed;

                    yield return null;
                }
                path.RemoveAt(0);
                yield return null;
            }
            agent.velocity = Vector2.zero;
        }
    }

    public void CancelNavigation()
    {
        StopCoroutine("StartNavigation");
    }
}
