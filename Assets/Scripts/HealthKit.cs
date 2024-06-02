using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float _healPower;

    public float HealPower => _healPower;
}
