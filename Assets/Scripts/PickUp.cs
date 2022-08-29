using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected bool CanPickUp = false;
    public PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        if (playerMovement.IsDead)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            playerMovement.MutAmount++;
        }
<<<<<<< HEAD
=======
        else
        {
            CanPickUp = false;
        }
    }
    
    void FixedUpdate() {
        if (CanPickUp && Input.GetKey("e"))
        {
            Destroy(GameObject.Find("greenPotion"));
        }    
>>>>>>> e8840368ee718df9fe51a51f5aa1a3b5026d4e37
    }
}