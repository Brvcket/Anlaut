using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShackingScript : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement.IsDead)
        {
            animator.SetBool("PlayerDead", true);
        }    else animator.SetBool("PlayerDead", false);
    }
}
