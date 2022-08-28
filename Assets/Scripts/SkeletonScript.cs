using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{

    public GameObject enemy;
    public Animator animator;
    public bool KnowWhereEnemy = false;
    public Rigidbody2D player;
    public bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (KnowWhereEnemy)
                    {
                        animator.SetBool("IsKnowWhereRat", true);
                        if (enemy.transform.position.x > player.transform.position.x + 0.6)
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x - 0.02f, enemy.transform.position.y);
                            enemy.transform.rotation.Set(180, 90, 180, 0);
                        } else if (enemy.transform.position.x < player.transform.position.x - 0.6)
                        {
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
            IsDead = true;
            animator.SetBool("IsDead", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
