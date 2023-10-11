using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private Coroutine _coroutine;

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
        if (_coroutine == null)
        {
           _coroutine= StartCoroutine(ChangeHealth(playerHealth + _healthChange));
        }
    }

    private void DecreaseHealth()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ChangeHealth(playerHealth - _healthChange));
        }
    }

    private IEnumerator ChangeHealth(float newHealth)
    {
        float currentHealth = playerHealth;
        float targetHealth = Mathf.Clamp(newHealth, _minHealth, _maxHealth);
        float healthSpeed = 1f;
        float healthComparisonThreshold = 0.001f;

        while (Mathf.Abs(currentHealth - targetHealth) > healthComparisonThreshold)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, healthSpeed * Time.deltaTime);
            playerHealth = currentHealth;
            HealthChanged?.Invoke(playerHealth);
            yield return null;
        }

        _coroutine = null;
    }
}