using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VampirismBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _vampirism.ChangedCharge += ChangeValue;
    }

    private void OnDisable()
    {
        _vampirism.ChangedCharge -= ChangeValue;
    }

    private void ChangeValue(float current, float max)
    {
        _slider.value = current / max;
    }
}
