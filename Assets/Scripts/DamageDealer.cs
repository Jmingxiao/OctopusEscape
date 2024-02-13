using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("OnTriggerEnter1");
        }
        Debug.Log("OnTriggerEnter2");

        // Check if the other object has a health component
        Health health = other.GetComponent<Health>();

        // If the other object has health, apply damage
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
