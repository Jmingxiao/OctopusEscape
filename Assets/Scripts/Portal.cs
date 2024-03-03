using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    public Transform cover;
    Vector2 oldpos;
    BoxCollider2D box;
    Animator anim;
    bool firsttime = true;
    bool activated = false;
    private void Start() {
        oldpos = cover.position;
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        anim.enabled = false;
    }


    private void Update() {
        if((cover.position.x> oldpos.x+3f||cover.position.x< oldpos.x-3f)&&firsttime)
        {
            firsttime = false;
            StartCoroutine(Openportal());
        }
     
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            if(activated)
            {
                anim.SetTrigger("Close");
                StartCoroutine(PlayerController.Instance.NextLevel());
            }
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
