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

    private void UpdateHealthBar(float newHealth)
    {
        StartCoroutine(ChangeHealthBar(newHealth));
    }

    private IEnumerator ChangeHealthBar(float newHealth)
    {
        float currentHealth = Bar.fillAmount;
        float targetHealth = newHealth;
        float HealthSpeed = 0.1f;

        while (Mathf.Abs(currentHealth - targetHealth) > 0.001f)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, HealthSpeed * Time.deltaTime);
            Bar.fillAmount = currentHealth;
            yield return null;
        }
    }
}
