using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Actor : MonoBehaviour
{
    private const int _minHealth = 0;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    public float Damage => _damage;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Actor enemy))
        {
            _currentHealth -= enemy.Damage;
            _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

            if(_currentHealth == _minHealth)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out HealthKit heal))
        {
            if (transform.TryGetComponent(out Player player))
            {
                _currentHealth += heal.HealPower;
                _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
                Destroy(heal.gameObject);
            }
        }
    }
}
