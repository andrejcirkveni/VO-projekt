using UnityEngine;
using Ilumisoft.HealthSystem;

public class FighterHealthBarUI : MonoBehaviour
{
    public Health health;             // referenca na Health komponentu igrača
    public HealthBarLite healthBar;   // health bar prefab u HUD-u

    void Start()
    {
        // postavi maksimalni health
        healthBar.SetMaxHealth(health.MaxHealth);

        // poveži da health bar prati promjene healtha
        health.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(float currentHealth)
    {
        healthBar.SetHealth(currentHealth);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= UpdateHealthBar;
    }
}

