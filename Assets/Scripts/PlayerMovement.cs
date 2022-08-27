using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float Speed, JumpForce, MaxSpeed, PowerMultiplier;

    protected bool LeftStrafe = false, RightStrafe = false, DoJump = false, LookLeft = false;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RightStrafe = Input.GetKey("d");
        LeftStrafe = Input.GetKey("a");
        DoJump = Input.GetKey("space") && Mathf.Abs(rb.velocity.y) < 0.001 && !DoJump;
    }

    private void FixedUpdate()
    {
        rb.freezeRotation = true;

        if (rb.velocity.y != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        
        if (rb.velocity.x > MaxSpeed)
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -MaxSpeed)
        {
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
        }
        
        if (RightStrafe)
        {
            rb.AddForce(new Vector2(Speed * 10, 0), ForceMode2D.Force);
            if (LookLeft)
            {
                LookLeft = false;
                transform.Rotate(new Vector3(180, 0, 180));
            }
        }
        if (LeftStrafe)
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
            rb.AddForce(new Vector2(0, JumpForce * PowerMultiplier), ForceMode2D.Impulse);
        }
    }
}
