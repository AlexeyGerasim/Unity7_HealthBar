using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Health _health;
    [SerializeField] private float _healthSpeed;

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnUpdateHealthBar;
    }

    private void OnDesible()
    {
        _health.HealthChanged -= OnUpdateHealthBar;
    }

    private void OnUpdateHealthBar(float newHealth)
    {
        StartCoroutine(ChangeHealthBar(newHealth));
    }

    private IEnumerator ChangeHealthBar(float newHealth)
    {
        float currentHealth = _bar.fillAmount;
        float targetHealth = newHealth;
        float healthComparisonThreshold = 0.001f;

        while (Mathf.Abs(currentHealth - targetHealth) > healthComparisonThreshold)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, _healthSpeed * Time.deltaTime);
            _bar.fillAmount = currentHealth;
            yield return null;
        }
    }
}