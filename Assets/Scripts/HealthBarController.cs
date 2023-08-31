using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public float health { get; private set; }
    private float MaxHealth = 1f;
    private float MinHealth = 0f;

    public static HealthBarController Instance { get; private set; }

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
        health += 0.1f;
        ChangeHealth(health);
    }

    private void DecreaseHealth()
    {
        health -= 0.1f;
        ChangeHealth(health);
    }

    private void ChangeHealth(float health)
    {
        health = Mathf.Clamp(health, MinHealth, MaxHealth);
    }
}
