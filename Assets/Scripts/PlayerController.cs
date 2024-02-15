using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float horizontal;
    private float jumpPower = 10.0f;

    private bool isfacingRight = true;

    private Rigidbody2D rb;
    private LayerMask groundLayer;
    [SerializeField]private Transform groundCheck;
    [SerializeField] private Transform sprite;

    public enum States
    {
        IsGrappling,
        None
    }
    [HideInInspector]public States m_state = States.None;

     public static PlayerController Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake() {
         if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");    
    }

    // Update is called once per frame
    void Update()
    {
       switch(m_state)
       {
            case States.None:
            horizontal = Input.GetAxis("Horizontal");
            if(Input.GetButtonDown("Jump") && IsGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            Flip();

            break;
            case States.IsGrappling:
            break;
       }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate() {
        switch(m_state)
       {
            case States.None:
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            break;
            case States.IsGrappling:
            break;
       }
        
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if(horizontal > 0 && !isfacingRight || horizontal < 0 && isfacingRight)
        {
            isfacingRight = !isfacingRight;
            Vector3 theScale = sprite.localScale;
            theScale.x *= -1;
            sprite.localScale = theScale;
        }
    }

}
