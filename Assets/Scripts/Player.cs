using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Controller _controller;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Controller>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(nameof(_controller.Direction), _controller.Direction);
        _rigidbody.velocity = new Vector2(_controller.Direction * Time.deltaTime * _speed, _controller.Jump * _jumpForce);
    }
}
