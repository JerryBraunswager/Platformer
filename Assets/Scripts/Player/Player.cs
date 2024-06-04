using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Animator), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private InputReader _controller;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<InputReader>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(nameof(_controller.Direction), _controller.Direction);
        _rigidbody.velocity = new Vector2(_controller.Direction * Time.deltaTime * _speed, _controller.Jump * _jumpForce);
    }
}
