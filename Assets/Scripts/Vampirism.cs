using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _workTime;
    [SerializeField] private float _healthAmount;
    [SerializeField] private float _radius;

    private Actor _hero;
    private bool _enabled = false;
    private float _timer = 0f;

    private void Awake()
    {
        _hero = GetComponent<Actor>();
    }

    private void Update()
    {
        if(_enabled == false && Input.GetKeyDown(KeyCode.Q)) 
        { 
            _enabled = true;
        }

        if (_enabled == true)
        {
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, _radius);

            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].gameObject.TryGetComponent(out Actor actor))
                {
                    actor.TakeDamage(_healthAmount * Time.deltaTime);
                    _hero.Heal(_healthAmount * Time.deltaTime);
                }
            }

            _timer += Time.deltaTime;

            if (_timer >= _workTime)
            {
                _enabled = false;
                _timer = 0f;
            }
        }
    }
}
