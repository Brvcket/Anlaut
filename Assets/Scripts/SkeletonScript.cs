using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{

    public GameObject enemy;
    public Animator animator;
    public bool KnowWhereEnemy = false, LookLeft = false;
    public Rigidbody2D player;
    public bool IsDead = false;
    public int health = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (KnowWhereEnemy) {
                animator.SetBool("IsKnowWhereRat", true);
                if (enemy.transform.position.x > player.transform.position.x + 0.6)
                {
                    if (LookLeft)
                    {
                        LookLeft = false;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }
                    enemy.transform.position = new Vector3(enemy.transform.position.x - 0.02f, enemy.transform.position.y);
                } else if (enemy.transform.position.x < player.transform.position.x - 0.6)
                {
                    if (!LookLeft)
                    {
                        LookLeft = true;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }
                    enemy.transform.position = new Vector3(enemy.transform.position.x + 0.02f, enemy.transform.position.y);
                } else
                {
                    animator.SetBool("IsReachRat", true);
                }
            } else
            {
                animator.SetBool("IsKnowWhereRat", false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet"){
            if (health == 0)
            {
                IsDead = true;
                animator.SetBool("IsDead", true);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else health--;
        }
    }
}
