using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBase : MonoBehaviour
{
     Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
     private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
            StartCoroutine(PlayerController.Instance.EndGame());
        }
    }
}
