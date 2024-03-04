using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void Start() {
        transform.DOMoveY(transform.position.y-10, 4).SetLoops(-1, LoopType.Yoyo);
    }
   private void Update() {
       
   }
}
