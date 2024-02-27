using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlySpick : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ReloadScene();
        }
    }
}