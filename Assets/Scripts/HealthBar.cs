using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image Bar;

    private void Start()
    {
        Health.Instance.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDesible()
    {
        Health.Instance.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float newHealth)
    {
        StartCoroutine(ChangeHealthBar(newHealth));
    }

    private IEnumerator ChangeHealthBar(float newHealth)
    {
        float currentHealth = Bar.fillAmount;
        float targetHealth = newHealth;
        float healthSpeed = 0.1f;
        float healthComparisonThreshold = 0.001f;

        while (Mathf.Abs(currentHealth - targetHealth) > healthComparisonThreshold)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, healthSpeed * Time.deltaTime);
            Bar.fillAmount = currentHealth;
            yield return null;
        }
    }
}