using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class npcFight : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider;  // Reference to the health slider (UI)

    void Start()
    {
        currentHealth = maxHealth;  // Set starting health
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;  // Set the initial slider value
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Decrease health by damage amount
        if (currentHealth <= 0f)
        {
            Die();
        }
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;  // Update health bar
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // Optionally, disable NPC, play death effects, etc.
        Destroy(gameObject);  // Deactivate NPC (or destroy, based on your game design)
    }
}
