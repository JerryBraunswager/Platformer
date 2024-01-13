using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Actor : MonoBehaviour
{
    private const int _minHealth = 0;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    public float Damage => _damage;

    private float _damageCount = 25;
    private float _healCount = 20;
    private float _currentHealth;

    public event UnityAction<float, float> HealthChanged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Actor enemy))
        {
            TakeDamage(enemy.Damage);

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
                Heal(heal.HealPower);
                Destroy(heal.gameObject);
            }
        }
    }

    public void ClickTakeDamageButton()
    {
        TakeDamage(_damageCount);
    }

    public void ClickHealButton()
    {
        Heal(_healCount);
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void Heal(float healPower)
    {
        _currentHealth += healPower;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}