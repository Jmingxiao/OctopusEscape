using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
         if(other.tag == "Player"){
            Debug.Log("Player has collected the item");
                PlayerController.Instance.AddScore(1);
              Destroy(gameObject);
         }
   }
}
