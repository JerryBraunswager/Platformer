using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _direction = Input.GetAxis(Horizontal);
        _animator.SetFloat(nameof(_direction), _direction);
        _rigidbody.velocity = new Vector2(_direction * Time.deltaTime * _speed, Input.GetAxis(Jump) * _jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
