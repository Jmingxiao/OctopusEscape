using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Update() {
        // Check if the player presses the "H" key
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Reduce the player's health by 10
            TakeDamage(10);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Check if health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death logic here
        Destroy(gameObject);
    }
}
