using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    protected bool CanOpen = false;

    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "table")
        {
            CanOpen = true;
        }
    }
    
    void FixedUpdate()
    {
        if (CanOpen && Input.GetKey("e"))
        {
            Debug.Log("Табличка");
        }    
    }
}