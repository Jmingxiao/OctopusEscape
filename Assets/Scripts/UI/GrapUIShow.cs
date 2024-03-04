using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapUIShow : MonoBehaviour
{
    bool firsttime = true;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")&&firsttime)
        {
            StartCoroutine( UITips.Instance.ShowGrapable());
            UITips.Instance.ShowMouseUI();
            firsttime = false;
        }
    }
}
