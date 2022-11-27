using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Agent : MonoBehaviour
{
    public bool IsNavigating { get; private set; } = false;

    [SerializeField] protected float _speed = 3f;
    [SerializeField] protected float _stoppingNodeDistance = 0.3f;
    [SerializeField] protected float _health = 5f;

    [Header("Navigation")]
    [SerializeField] protected Pathfinder _pathfinder;

    private Rigidbody2D _rb;
    private Vector2 _currentTarget;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void StartNavigation(Transform to)
    {
        StartNavigation(new Vector2(to.position.x, to.position.y));
    }
    public void StartNavigation(Vector2 to)
    {
        CancelNavigation();

        this._rb.velocity = Vector2.zero;

        //Defensive clause if the _pathfinder is not set
        if (_pathfinder == null)
        {
            Debug.LogWarning("WARNING: Pathfinder variable was not set. Looking for the pathfinder object...");
            _pathfinder = FindObjectOfType<Pathfinder>();

            if (_pathfinder == null)
            {
                Debug.LogError("ERROR: There's no pathfinder object in the scene");
                return;
            }
            else Debug.Log("LOG: Pathfinder found. Resuming execution...");
        }

        StartCoroutine(NavigationRoutine(to, _speed, _stoppingNodeDistance));
    }

    public bool IsNavigatingTowards(Vector2 towards)
    {
        return IsNavigating && towards == _currentTarget;
    }

    IEnumerator NavigationRoutine(Vector2 to, float speed, float stoppingNodeDistance)
    {
        List<Node> path = _pathfinder.GetPath(_rb, to);

        if (path != null)
        {
            IsNavigating = true;
            _currentTarget = to;

            while (path.Count > 0)
            {
                while (Vector2.Distance(_rb.position, path[0].GetPosition()) > stoppingNodeDistance)
                {
                    //Move towards next node
                    Vector2 movementDir = path[0].GetPosition() - _rb.position;
                    _rb.velocity = movementDir.normalized * speed;

                    yield return null;
                }
                path.RemoveAt(0);
                yield return null;
            }
            _rb.velocity = Vector2.zero;

            IsNavigating = false;
            _currentTarget = Vector2.zero;
        }
    }

    public void CancelNavigation()
    {
        StopCoroutine(nameof(NavigationRoutine));
        _rb.velocity = Vector2.zero;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health < 0) Die();
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
