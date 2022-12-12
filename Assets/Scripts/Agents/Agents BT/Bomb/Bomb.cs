using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const string EXPLOSION_TRIGGER_NAME = "Explode";

    [SerializeField] float _damage = 2f;
    [SerializeField] float _explosionRange = 1f;
    [SerializeField] float _explosionDelay = 1f;
    [SerializeField] float _explosionBackForce = 2f;

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
            GetComponent<AudioSource>().Play();
        } 
    }

    private void DamageEntitiesInRange()
    {
        EntityHealth[] entities = FindObjectsOfType<EntityHealth>();

        foreach (var entity in entities)
        {
            if (Vector2.Distance(entity.transform.position, transform.position) < _explosionRange &&
                !entity.gameObject.CompareTag(gameObject.tag))
            {
                entity.TakeDamage(_damage);

                Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();

                if (rb != null) 
                {
                    Vector2 explosionDir = (entity.transform.position - transform.position).normalized;
                    rb.AddForce(explosionDir * _explosionBackForce);
                }
            }
        }
    }

    private void DestroyInstance()
    {
        Destroy(this.gameObject);
    }
}
