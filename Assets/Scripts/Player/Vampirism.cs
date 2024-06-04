using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputReader), typeof(Actor))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private Transform _vampirismView;
    [SerializeField] private float _workTime;
    [SerializeField] private float _healthAmount;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;
    [SerializeField] private LayerMask _layerMask;

    private InputReader _inputReader;
    private Coroutine _coroutine;
    private float _timer = 0f;
    private WaitForSeconds _delaySeconds;
    private Actor _player;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _player = GetComponent<Actor>();
    }

    private void Start()
    {
        _delaySeconds = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _inputReader.SpellButtonClicked += ActiveSpell;
    }

    private void OnDisable()
    {
        _inputReader.SpellButtonClicked -= ActiveSpell;
    }

    public event UnityAction<float, float> ChangedCharge;

    private void ActiveSpell()
    {
        if(_coroutine == null) 
        {
            _timer = 0f;
            _coroutine = StartCoroutine(EnableSpell());
            _vampirismView.gameObject.SetActive(true);
            ChangedCharge?.Invoke(_timer, _workTime);
        }
    }

    private IEnumerator EnableSpell()
    {
        while(_timer < _workTime)
        {
            _timer += _delay;
            TransferHealth(FindEnemies());
            ChangedCharge?.Invoke(_timer, _workTime);
            yield return _delaySeconds;
        }

        _coroutine = null;
        _vampirismView.gameObject.SetActive(false);
    }

    private void TransferHealth(Collider2D[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.TryGetComponent(out Actor actor))
            {
                float damage = (actor.Health < _healthAmount) ? actor.Health : _healthAmount;
                actor.TakeDamage(damage);
                _player.Heal(damage);
            }
        }
    }

    private Collider2D[] FindEnemies()
    {
        return Physics2D.OverlapCircleAll(transform.position, _radius, _layerMask);
    }
}
