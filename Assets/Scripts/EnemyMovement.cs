using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _bias;
    [SerializeField] private float _raycastLength;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private float _speed;

    private bool _isEndPositionAchieved = false;
    private bool _isRight;
    private Animator _animator;
    private Vector3 _direction;
    private Vector3 _biasVector;
    private Vector3 _target;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit2D hits = Physics2D.Raycast(transform.position + _biasVector, _direction, _raycastLength);

        if (hits == true)
        {
            MoveToHero(hits, out _target, out _direction, out _biasVector);
        }
        else
        {
            MoveToTarget(out _target, out _direction, out _biasVector);
        }

        _animator.SetBool(nameof(_isRight), _isRight);
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void MoveToTarget(out Vector3 target, out Vector3 direction, out Vector3 bias)
    {
        if (_isEndPositionAchieved == false)
        {
            target = _endPosition.position;

            if (transform.position.x == _endPosition.position.x)
            {
                _isEndPositionAchieved = true;
            }
        }
        else
        {
            target = _startPosition.position;

            if (transform.position == _startPosition.position)
            {
                _isEndPositionAchieved = false;
            }
        }

        ChooseDirection(target, out direction, out bias);
    }

    private void ChooseDirection(Vector3 target, out Vector3 direction, out Vector3 biasVector)
    {
        if(transform.position.x < target.x)
        {
            MoveRight(out direction, out biasVector);
        }
        else
        {
            MoveLeft(out direction, out biasVector);
        }
    }

    private void MoveToHero(RaycastHit2D hits, out Vector3 target, out Vector3 direction, out Vector3 bias)
    {
        target = hits.collider.transform.position;

        if (hits.transform.position.x > transform.position.x)
        {
            MoveRight(out direction, out bias);
        }
        else
        {
            MoveLeft(out direction, out bias);
        }
    }

    private void MoveLeft(out Vector3 direction, out Vector3 biasVector)
    {
        _isRight = false;
        direction = Vector3.left;
        biasVector = new Vector3(-_bias, 0, 0);
    }

    private void MoveRight(out Vector3 direction, out Vector3 biasVector)
    {
        _isRight = true;
        direction = Vector3.right;
        biasVector = new Vector3(_bias, 0, 0);
    }
}
