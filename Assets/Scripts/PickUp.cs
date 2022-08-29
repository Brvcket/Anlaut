using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected bool CanPickUp = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "pickup")
        {
            CanPickUp = true;
        }
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
    }
}