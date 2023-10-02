using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    public float health { get; private set; }
    private float MaxHealth = 1f;
    private float MinHealth = 0f;
    private float HealthChange = 0.1f;

    public event System.Action<float> OnHealthChanged;

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
        if (!IsInvoking("ChangeHealthCoroutine"))
        {
            StartCoroutine(ChangeHealthCoroutine(health + HealthChange));
        }
    }

    private void DecreaseHealth()
    {
        if (!IsInvoking("ChangeHealthCoroutine"))
        {
            StartCoroutine(ChangeHealthCoroutine(health - HealthChange));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float newHealth)
    {
        float currentHealth = health;
        float targetHealth = Mathf.Clamp(newHealth, MinHealth, MaxHealth);
        float HealthSpeed = 0.1f;

        while (Mathf.Abs(currentHealth - targetHealth) > 0.001f)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, HealthSpeed * Time.deltaTime);
            health = currentHealth;
            OnHealthChanged?.Invoke(health);
            yield return null;
        }

        yield break;
    }
}
