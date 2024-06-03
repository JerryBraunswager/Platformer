using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Actor))]
public class Vampirism : MonoBehaviour
{

    [SerializeField] private float _workTime;
    [SerializeField] private float _healthAmount;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;

    private const string EnemyLayer = "Enemy";

    private Controller _controller;
    private Coroutine _coroutine;
    private float _timer = 0f;
    private WaitForSeconds _delaySeconds;
    private Actor _player;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _player = GetComponent<Actor>();
    }

    private void Start()
    {
        _delaySeconds = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _controller.SpellButtonClicked += ActiveSpell;
    }

    private void OnDisable()
    {
        _controller.SpellButtonClicked -= ActiveSpell;
    }

    private void ActiveSpell()
    {
        if(_coroutine == null) 
        {
            _timer = 0f;
            _coroutine = StartCoroutine(EnableSpell());
        }
    }

    private IEnumerator EnableSpell()
    {
        while(_timer < _workTime)
        {
            _timer += _delay;
            DamageEnemies();
            yield return _delaySeconds;
        }

        if (_timer >= _workTime)
        {
            _coroutine = null;
        }
    }

    private void DamageEnemies()
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, _radius, LayerMask.GetMask(EnemyLayer));

        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i].gameObject.TryGetComponent(out Actor actor))
            {
                float damage = actor.TakeDamage(_healthAmount);
                _player.Heal(damage);
            }
        }
    }
}
