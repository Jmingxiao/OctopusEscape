using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Portal : MonoBehaviour
{
    public TilemapRenderer cover;
    BoxCollider2D box;
    Animator anim;
    bool firsttime = true;
    bool activated = false;
    private void Start() {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        anim.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            if(firsttime)
            {
                cover.enabled = false;
                StartCoroutine(Openportal());
            }
            else if(activated)
            {
                anim.SetTrigger("Close");
                StartCoroutine(PlayerController.Instance.NextLevel());
            }
            firsttime =false;
        }
    }

    IEnumerator Openportal()
    {
        anim.enabled = true;
        box.size = new Vector2(0.1f, 0.2f);
        var a = anim.GetCurrentAnimatorClipInfo(0);
    
        while(a[0].clip.name!= "Portal")
        {   
            Debug.Log(a[0].clip.name);
            a = anim.GetCurrentAnimatorClipInfo(0);
            yield return null;
        }
        activated = true;
    }

}
