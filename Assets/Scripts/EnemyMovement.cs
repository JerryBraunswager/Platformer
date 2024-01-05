using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speed;

    private bool _isEndPositionAchieved = false;
    private bool _isRight;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 target;

        if (_isEndPositionAchieved == false)
        {
            target = _endPosition.position;
            _isRight = true;

            if (transform.position.x == _endPosition.position.x)
            {
                _isEndPositionAchieved = true;
            }
        }
        else
        {
            target = _startPosition.position;
            _isRight = false;

            if (transform.position == _startPosition.position)
            {
                _isEndPositionAchieved = false;
            }
        }

        _animator.SetBool(nameof(_isRight), _isRight);
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }
}
