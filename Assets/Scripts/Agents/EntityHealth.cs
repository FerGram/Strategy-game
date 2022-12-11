using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] public float _hp = 5f;
    private EntityHealthUI _healthUI;

    private void Start()
    {
        _healthUI = GetComponentInChildren<EntityHealthUI>();

        if (_healthUI != null) _healthUI.SetUpSlider(_hp);
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_healthUI != null) _healthUI.UpdateHealthUI(_hp);
        if (_hp <= 0) Destroy(gameObject);
    }

    public float GetHealth() => _hp;
}
