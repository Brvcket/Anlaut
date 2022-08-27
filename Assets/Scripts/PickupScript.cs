using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_script : MonoBehaviour
{
    public float PowerMultiplier;
    protected bool CanPickUp = false;
    protected gameObject Object;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "pickup")
        {
            CanPickUp = true;
        }
    }
    
    void FixedUpdate(){
        if (CanPickUp && Input.GetKey("e"))
        {
            PowerMultiplier += 5;
        }    
    }
}
