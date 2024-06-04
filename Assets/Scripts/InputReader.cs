using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const string HorizontalString = "Horizontal";
    private const string JumpString = "Jump";

    [SerializeField] private KeyCode _spellKey;

    private float _direction;
    private float _jump;

    public float Direction => _direction;
    public float Jump => _jump;

    private void Update()
    {
        _jump = Input.GetAxis(JumpString);
        _direction = Input.GetAxis(HorizontalString);

        if (Input.GetKeyDown(_spellKey))
        {
            SpellButtonClicked?.Invoke();
        }

    }

    public event UnityAction SpellButtonClicked;
}
