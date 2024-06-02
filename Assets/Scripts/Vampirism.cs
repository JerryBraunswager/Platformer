using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _workTime;
    [SerializeField] private float _healthAmount;
    [SerializeField] private float _radius;
    [SerializeField] private KeyCode _key;

    private Actor _hero;
    private Coroutine _coroutine;
    private float _timer = 0f;

    private void Awake()
    {
        _hero = GetComponent<Actor>();
    }

    private void Update()
    {
        Debug.Log(_coroutine);
        if(_coroutine == null && Input.GetKeyDown(_key)) 
        {
            _timer = 0f;
            _coroutine = StartCoroutine(StartWork());
        }
    }

    private IEnumerator StartWork()
    {
        while(_timer < _workTime)
        {
            _timer += Time.deltaTime;
            GetEnemies();
            yield return null;

            if(_timer >= _workTime)
            {
                _coroutine = null;
            }
        }
    }

    private void GetEnemies()
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
    }
}
