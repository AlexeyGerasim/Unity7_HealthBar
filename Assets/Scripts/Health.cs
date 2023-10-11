using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxHealth = 1f;
    private float _minHealth = 0f;
    private float _healthChange = 0.1f;

    public event UnityAction<float> HealthChanged;
    private float playerHealth;

    private void Start()
    {
        playerHealth = _maxHealth;
    }

    private void IncreaseHealth()
    {
        float newHealth = playerHealth + _healthChange;
        newHealth = Mathf.Clamp(newHealth, _minHealth, _maxHealth);
        UpdateHealth(newHealth);
    }

    private void DecreaseHealth()
    {
        float newHealth = playerHealth - _healthChange;
        newHealth = Mathf.Clamp(newHealth, _minHealth, _maxHealth);
        UpdateHealth(newHealth);
    }

    private void UpdateHealth(float newHealth)
    {
        playerHealth = newHealth;
        HealthChanged?.Invoke(playerHealth);
    }
}