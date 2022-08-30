using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private AudioSource aud;

    
    public int MutAmount = 0;
    public float Speed = 8, JumpForce = 10, MaxSpeed = 2.5f;
    protected float StartX, StartY;

    protected bool DoJump = false, LookLeft = false;
    public bool IsJumping = false, IsJumpTurning = false, IsDead = false, IsReborn = false, AbleToMove = true, MustLand = false; // For animations

    private Animator animator;

    public Transform FirePointPosition;
    public GameObject Laser;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        aud.Stop();
        StartX = gameObject.transform.position.x;
        StartY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MaxSpeed = 2.5f + MutAmount * 0.2f;
        if (Input.GetKeyDown("d") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("d") || DoJump) {aud.Stop();}
        if (Input.GetKeyDown("a") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("a") || DoJump) {aud.Stop();}
        if (Input.GetKeyDown("mouse 0") && !IsDead)
        {
            for (int i = 0; i < MutAmount + 1; i++)
            {
                Instantiate(Laser, FirePointPosition.position, FirePointPosition.rotation);
            } 
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "enemy")
        {
            AbleToMove = false;
            animator.SetBool("IsDead", true);
            IsDead = true;
        }
    }

    private void FixedUpdate()
    {
        DoJump = Input.GetKey("space") && Mathf.Abs(rb.velocity.y) < 0.001 && !DoJump;
        if (rb.velocity.y != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
        if (rb.velocity.x > MaxSpeed)
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -MaxSpeed)
        {
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
        }
        
        
        

        // movement/animation block
        if (Input.GetKey("d") && AbleToMove)
        {
            IsReborn = false;
            rb.AddForce(new Vector2(Speed * (10 + MutAmount), 0), ForceMode2D.Force);
            if (LookLeft)
            {
                animator.SetBool("IsReborn", false);
                LookLeft = false;
                transform.Rotate(new Vector3(180, 0, 180));
                
            }
        }
        if (Input.GetKey("a") && AbleToMove)
        {
            IsReborn = false;
            rb.AddForce(new Vector2(-Speed * (10 + MutAmount), 0), ForceMode2D.Force);
            if (!LookLeft)
            {
                animator.SetBool("IsReborn", false);
                LookLeft = true;
                transform.Rotate(new Vector3(180, 0, 180));
                
            }
        }
        if (Input.GetKey("r"))
        {
            if (IsDead)
            {
                IsReborn = true;
                animator.SetBool("IsDead", false);
                IsDead = false;
                animator.SetBool("IsReborn", true);
                AbleToMove = true;
                rb.position = new Vector2(StartX, StartY);
                MutAmount = 0;
            }
        }
        if (DoJump && AbleToMove)
        {
            IsJumping = true;
            animator.SetBool("IsJumping", IsJumping);
            MustLand = true;
            IsReborn = false;
            animator.SetBool("IsReborn", false);
            rb.AddForce(new Vector2(0, JumpForce + MutAmount * 0.5f), ForceMode2D.Impulse);
            
            animator.SetBool("IsLanding", false);
        } 
        if (IsJumping)
        {
            if (rb.velocity.y > -1 && rb.velocity.y <= 1)
            {
                IsJumping = false;
                IsJumpTurning = true;
                animator.SetBool("IsJumping", IsJumping);
                animator.SetBool("IsJumpTurning", IsJumpTurning);
            }
        } 
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
