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

    public float Speed, JumpForce, MaxSpeed;

    protected bool DoJump = false, LookLeft = false;
    protected bool IsJumping = false, IsJumpTurning = false, IsLanding = false, IsDead = false, IsReborn = false, AbleToMove = true; // For animations

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        aud.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("d") || DoJump) {aud.Stop();}
        if (Input.GetKeyDown("a") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("a") || DoJump) {aud.Stop();}
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            AbleToMove = false;
            animator.SetBool("IsDead", true);
        }
    }

    private void FixedUpdate()
    {
        DoJump = Input.GetKey("space") && Mathf.Abs(rb.velocity.y) < 0.001 && !DoJump;
        if (rb.velocity.y != 0)
        {
            animator.SetBool("IsMoving", true);
            IsLanding = false;
        }
        else
        {
            animator.SetBool("IsMoving", false);
            IsLanding = false;
        }
        
        if (rb.velocity.x > MaxSpeed)
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -MaxSpeed)
        {
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
        }
        
        if (Input.GetKey("d") && AbleToMove)
        {
            rb.AddForce(new Vector2(Speed * 10, 0), ForceMode2D.Force);
            if (LookLeft)
            {
                LookLeft = false;
                animator.SetBool("IsLanding", IsLanding);
                animator.SetBool("IsReborn", false);
                transform.Rotate(new Vector3(180, 0, 180));
            }
        }
        if (Input.GetKey("a") && AbleToMove)
        {
            rb.AddForce(new Vector2(-Speed * 10, 0), ForceMode2D.Force);
            if (!LookLeft)
            {
                LookLeft = true;
                animator.SetBool("IsLanding", IsLanding);
                animator.SetBool("IsReborn", false);
                transform.Rotate(new Vector3(180, 0, 180));
            }
        }
        if (Input.GetKey("r"))
        {
            animator.SetBool("IsDead", false);
            animator.SetBool("IsReborn", true);
            AbleToMove = true;
            rb.position = new Vector2(-4.4f, -5);
        }
        if (DoJump && AbleToMove)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            IsJumping = true;
            IsLanding = false;
            animator.SetBool("IsLanding", IsLanding);
            animator.SetBool("IsJumping", IsJumping);
        } 
        if (IsJumping)
        {
            if (rb.velocity.y > -1 && rb.velocity.y <= 1)
            {
                IsJumping = false;
                IsJumpTurning = true;
                animator.SetBool("IsJumping", IsJumping);
                animator.SetBool("IsJumpTurning", IsJumpTurning);
                IsLanding = true;
                animator.SetBool("IsLanding", IsLanding);
            }
        } else if (IsJumpTurning)
        {
            if (rb.velocity.y > -0.04 && rb.velocity.y <= 0.04)
            {
                IsJumpTurning = false;
                IsLanding = true;
                animator.SetBool("IsJumpTurning", IsJumpTurning);
                animator.SetBool("IsLanding", IsLanding);
                IsLanding = false;
            }
        }
    }
}
