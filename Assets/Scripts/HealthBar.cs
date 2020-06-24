using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health healthBehaviour = null;

    [Header("UI")]
    [SerializeField] private Image healthBarImage = null;

    private void OnEnable()
    {
        UpdateHealthBar(healthBehaviour.CurrentHealth, healthBehaviour.MaxHealth);

        healthBehaviour.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        healthBehaviour.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(object sender, Health.HealthChangedEventArgs e)
    {
        UpdateHealthBar(e.CurrentHealth, e.MaxHealth);
    }

    private void UpdateHealthBar(float health, float maxHealth)
    {
        healthBarImage.fillAmount = health / maxHealth;
    }
}
