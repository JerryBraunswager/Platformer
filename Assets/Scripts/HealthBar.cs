using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Actor _actor;

    private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
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
        _healthSlider.value = (float)currentHealth / maxHeath;
    }
}
