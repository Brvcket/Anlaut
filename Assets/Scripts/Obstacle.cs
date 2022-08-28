using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool YouAreDead = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            animator.SetBool("IsDead", true);
            /*gameObject.
            Destroy(gameObject);*/
            YouAreDead = true;
        }
    }

}