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
    protected bool IsJumping = false, IsJumpTurning = false, IsLanding = false; // For animations

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
        DoJump = Input.GetKey("space") && Mathf.Abs(rb.velocity.y) < 0.001 && !DoJump;
        if (Input.GetKeyDown("d") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("d") || DoJump) {aud.Stop();}
        if (Input.GetKeyDown("a") && !DoJump) {aud.Play();}
        else if (Input.GetKeyUp("a") || DoJump) {aud.Stop();}
    }

    private void FixedUpdate()
    {
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
        
        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed * 10, 0), ForceMode2D.Force);
            if (LookLeft)
            {
                LookLeft = false;
                transform.Rotate(new Vector3(180, 0, 180));
            }
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed * 10, 0), ForceMode2D.Force);
            if (!LookLeft)
            {
                LookLeft = true;
                transform.Rotate(new Vector3(180, 0, 180));
            }
        }
        if (DoJump)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            IsJumping = true;
            IsLanding = false;
            animator.SetBool("IsJumping", IsJumping);
        } 
        if (IsJumping)
        {
            if (rb.velocity.y < 0.05)
            {
                IsJumping = false;
                IsJumpTurning = true;
                animator.SetBool("IsJumping", IsJumping);
                animator.SetBool("IsJumpTurning", IsJumpTurning);
            }
        } else if (IsJumpTurning)
        {
            if (rb.velocity.y > -0.02)
            {
                IsJumpTurning = false;
                IsLanding = true;
                animator.SetBool("IsJumpTurning", IsJumpTurning);
                animator.SetBool("IsLanding", IsLanding);
            }
        }
    }
}
