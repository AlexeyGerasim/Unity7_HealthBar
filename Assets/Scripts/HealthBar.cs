using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image Bar;

    private float targetHealth;
    private float currentHealth;
    private float HealthSpeed = 0.1f;

    void Update()
    {
        currentHealth = Bar.fillAmount;
        targetHealth = HealthBarController.Instance.health;
        Bar.fillAmount = Mathf.MoveTowards(currentHealth, targetHealth, HealthSpeed * Time.deltaTime);
    }
}
