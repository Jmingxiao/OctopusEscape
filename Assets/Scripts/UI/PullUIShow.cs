using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUIShow : MonoBehaviour
{
    bool firsttime = true;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")&&firsttime)
        {
            UITips.Instance.ShowPullable();
            firsttime = false;
        }
    }
}
