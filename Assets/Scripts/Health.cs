using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const string CommandChangeHealthCoroutine = "ChangeHealthCoroutine";
    private float _maxHealth = 1f;
    private float _minHealth = 0f;
    private float _healthChange = 0.1f;

    public event System.Action<float> HealthChanged;
    public float health { get; private set; }

    public static Health Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = _maxHealth;
    }

    private void IncreaseHealth()
    {
        if (!IsInvoking(CommandChangeHealthCoroutine))
        {
            StartCoroutine(ChangeHealthCoroutine(health + _healthChange));
        }
    }

    private void DecreaseHealth()
    {
        if (!IsInvoking(CommandChangeHealthCoroutine))
        {
            StartCoroutine(ChangeHealthCoroutine(health - _healthChange));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float newHealth)
    {
        float currentHealth = health;
        float targetHealth = Mathf.Clamp(newHealth, _minHealth, _maxHealth);
        float HealthSpeed = 0.1f;
        float healthComparisonThreshold = 0.001f;

        while (Mathf.Abs(currentHealth - targetHealth) > healthComparisonThreshold)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, HealthSpeed * Time.deltaTime);
            health = currentHealth;
            HealthChanged?.Invoke(health);
            yield return null;
        }

        yield break;
    }
}