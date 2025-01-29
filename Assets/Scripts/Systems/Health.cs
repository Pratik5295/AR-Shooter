using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f; // Maximum health value
    private float currentHealth;

    public float GetCurrentHealth() => currentHealth;

    [Header("Optional Components")]
    public bool destroyOnDeath = true; // Should the object be destroyed on death?
    public GameObject deathEffect;    // Effect to instantiate on death (e.g., explosion, particle effect)

    void Start()
    {
        // Initialize health
        ResetHealth();
    }

    /// <summary>
    /// Takes damage and reduces health.
    /// </summary>
    /// <param name="damage">Amount of damage to take.</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays within bounds
        Debug.Log($"{gameObject.name} took {damage} damage. Current Health: {currentHealth}");

        // Check if dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Resets health to maximum.
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"{gameObject.name}'s health reset to {currentHealth}");
    }

    /// <summary>
    /// Handles death logic.
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // Optional: Instantiate death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Optional: Destroy object on death
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Heals the object by a given amount.
    /// </summary>
    /// <param name="healAmount">Amount to heal.</param>
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays within bounds
        Debug.Log($"{gameObject.name} healed {healAmount}. Current Health: {currentHealth}");
    }
}
