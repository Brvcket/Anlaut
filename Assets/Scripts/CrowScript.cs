using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowScript : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject enemy;
    public GameObject StartPosition;
    public Animator animator;
    public bool KnowWhereEnemy = false;
    public Rigidbody2D player;
    public bool IsDead = false, SameX = false, SameY = false, LookLeft = false;
    public int health = 4;
    protected float StartX, StartY;
    // Start is called before the first frame update
    void Start()
    {
        StartX = enemy.transform.position.x;
        StartY = enemy.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (KnowWhereEnemy)
            {
                if (enemy.transform.position.y > player.transform.position.y + 0.3)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 0.05f);
                    SameY = false;
                }
                else if (enemy.transform.position.y < player.transform.position.y)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.05f);
                    SameY = false;
                }
                else SameY = true;
                if (enemy.transform.position.x > player.transform.position.x + 0.3)
                {
                    if (!LookLeft)
                    {
                        LookLeft = true;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }  
                    enemy.transform.position = new Vector3(enemy.transform.position.x - 0.03f, enemy.transform.position.y);
                    SameX = false;
                } else if (enemy.transform.position.x < player.transform.position.x - 0.3)
                {
                    if (LookLeft)
                    {
                        LookLeft = false;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }
                    enemy.transform.position = new Vector3(enemy.transform.position.x + 0.03f, enemy.transform.position.y);
                    SameX = false;
                } else SameX = true;
                if (SameY && SameX)
                {
                    animator.SetBool("IsReachRat", true);
                } else animator.SetBool("IsReachRat", false);
            } else
            {
                animator.SetBool("IsReachRat", false);
                if (enemy.transform.position.y > StartPosition.transform.position.y + 0.3)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 0.05f);
                }
                else if (enemy.transform.position.y < StartPosition.transform.position.y - 0.3)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.05f);
                }
                else SameY = true;
                if (enemy.transform.position.x > StartPosition.transform.position.x + 0.3)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x - 0.03f, enemy.transform.position.y);
                }
                else if (enemy.transform.position.x < StartPosition.transform.position.x - 0.3)
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x + 0.03f, enemy.transform.position.y);
                }
                else SameX = true;
            }
        }
        else
        {
            if (playerMovement.IsReborn)
            {
                enemy.transform.position = new Vector3(StartX, StartY);
                animator.SetBool("RatReborn", true);
                IsDead = false;
                animator.SetBool("IsDead", false);
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
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
