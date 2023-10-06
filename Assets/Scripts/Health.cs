using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const string CommandChangeHealthCoroutine = "ChangeHealthCoroutine";
    public float health { get; private set; }
    private float MaxHealth = 1f;
    private float MinHealth = 0f;
    private float HealthChange = 0.1f;

    public event System.Action<float> HealthChanged;

    public static Health Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        health = MaxHealth;
    }

    private void IncreaseHealth()
    {
        if (!IsInvoking(CommandChangeHealthCoroutine))
        {
            StartCoroutine(ChangeHealthCoroutine(health + HealthChange));
        }
    }

    private void DecreaseHealth()
    {
        if (!IsInvoking(CommandChangeHealthCoroutine))
        {
            StartCoroutine(ChangeHealthCoroutine(health - HealthChange));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float newHealth)
    {
        float currentHealth = health;
        float targetHealth = Mathf.Clamp(newHealth, MinHealth, MaxHealth);
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