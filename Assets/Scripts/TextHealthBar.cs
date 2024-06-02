using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private Actor _actor;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _actor.HealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
        _actor.HealthChanged -= DisplayHealth;
    }

    private void DisplayHealth(float currentHealth, float maxHeath)
    {
        _text.text = currentHealth + "/" + maxHeath;
    }
}
