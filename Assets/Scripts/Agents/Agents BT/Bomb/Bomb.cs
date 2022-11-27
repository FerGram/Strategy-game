using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const string EXPLOSION_TRIGGER_NAME = "Explode";

    [SerializeField] float _damage;
    [SerializeField] float _explosionRange;
    [SerializeField] float _explosionDelay;

    private Animator _animator;

    private float _currentTime = 0;
    private bool _hasExploded = false;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();

        //If animator is not null, we'll use animation events. Otherwise, just call it inmediately
        if (_animator == null)
        {
            Invoke(nameof(DamageEntitiesInRange), _explosionDelay);
            Invoke(nameof(DestroyInstance), _explosionDelay + 0.5f);
        }
    }

    private void Update()
    {
        if (_currentTime < _explosionDelay) _currentTime += Time.deltaTime;
        else if (!_hasExploded)
        {
            _animator.SetTrigger(EXPLOSION_TRIGGER_NAME);
            _hasExploded = true;
        } 
    }

    private void DamageEntitiesInRange()
    {
        Agent[] agents = FindObjectsOfType<Agent>();

        foreach (var agent in agents)
        {
            if (Vector2.Distance(agent.transform.position, transform.position) < _explosionRange)
            {
                agent.TakeDamage(_damage);
            }
        }
    }

    private void DestroyInstance()
    {
        Destroy(this.gameObject);
    }
}
