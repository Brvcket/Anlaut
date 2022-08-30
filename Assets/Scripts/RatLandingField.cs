using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatLandingField : MonoBehaviour
{

    public PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "enemy" && collision.tag != "pickup")
        {
            playerMovement.GetAnimator().SetBool("IsJumpTurning", false);
            if (playerMovement.MustLand)
            {
                playerMovement.GetAnimator().SetBool("IsLanding", true);
                playerMovement.MustLand = false;
            } else
            {
                playerMovement.GetAnimator().SetBool("IsLanding", false);
            }
        }
    }
}
