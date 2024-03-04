using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool HasJumping { get => hasjumping; set => hasjumping = value; }
    private float speed = 10.0f;
    private float horizontal;
    private bool hasjumping = false;
    private bool isfacingRight = true;
    private float score = 0;
    public float GetScore { get => score; }
    public void AddScore(float value){ score += value; }
    private Rigidbody2D rb;
    private LayerMask groundLayer;
    [SerializeField]private Transform groundCheck;
    [SerializeField] private Transform sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private float gravityScale = 3f;
    [HideInInspector] public float glidingCooldown;
    public enum States
    {
        IsDead,
        IsGrappling,
        IsGliding,
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
        m_state = States.None; 
    }

    // Update is called once per frame
    void Update()
    {
        bool isgliding =false;
        switch(m_state)
        {
            case States.None:
            rb.gravityScale = gravityScale;
            horizontal = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(horizontal));
            isgliding = Input.GetButton("Jump")&&rb.velocity.y<0.1f&&!IsGround();
            if(isgliding&&glidingCooldown>0.3f)
            {
                rb.gravityScale = 0.5f;
                rb.velocity = new Vector2(rb.velocity.x, -1f);
            }else{
                rb.gravityScale = gravityScale;
            }
            Flip();
            break;
            case States.IsGrappling:
            rb.gravityScale = gravityScale;
            horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector2(horizontal*0.1f, 0));
            break;
            case States.IsDead:
            rb.velocity = Vector2.zero;
            break;
        }
        if(isgliding&&glidingCooldown>0.0f)
        {
            glidingCooldown -= glidingCooldown<-0.01f?0:Time.deltaTime*0.5f;
        }
        else
        {
            glidingCooldown += glidingCooldown>1f?0:Time.deltaTime*3f;
        }
        anim.SetBool("Gliding", isgliding&&glidingCooldown>0.3f);
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

    public IEnumerator Die()
    {   
        m_state = States.IsDead;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ReloadScene();
    }
    public IEnumerator NextLevel()
    {   
        m_state = States.IsDead;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadNextScene();
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
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
