using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolemScript : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject enemy;
    public Animator animator;
    public bool KnowWhereEnemy = false;
    public Rigidbody2D player;
    public Rigidbody2D golem;
    public bool IsDead = false, SameX = false, LookLeft = false;
    protected bool DoJump = false;
    public int health = 20;
    protected int MaxHealth;
    protected float StartX, StartY;
    // Start is called before the first frame update
    void Start()
    {
        StartX = enemy.transform.position.x;
        StartY = enemy.transform.position.y;
        MaxHealth = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoJump =  Mathf.Abs(golem.velocity.y) < 0.01 && !DoJump;
        if (playerMovement.IsReborn)
        {
            enemy.transform.position = new Vector3(StartX, StartY);
            animator.SetBool("RatReborn", true);
            IsDead = false;
            animator.SetBool("IsDead", false);
            GetComponent<BoxCollider2D>().enabled = true;
            health = MaxHealth;
        }
        if (!IsDead)
        {
            animator.SetBool("IsDead", false);
            if (KnowWhereEnemy)
            {
                if (DoJump) golem.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
                animator.SetBool("IsKnowWhereRat", true);
                if (enemy.transform.position.x > player.transform.position.x + 0.6)
                {
                    if (LookLeft)
                    {
                        LookLeft = false;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }
                    enemy.transform.position = new Vector3(enemy.transform.position.x - 0.015f, enemy.transform.position.y);
                }
                else if (enemy.transform.position.x < player.transform.position.x - 0.6)
                {
                    if (!LookLeft)
                    {
                        LookLeft = true;
                        transform.Rotate(new Vector3(180, 0, 180));
                    }
                    enemy.transform.position = new Vector3(enemy.transform.position.x + 0.015f, enemy.transform.position.y);
                }
                else
                {
                    animator.SetBool("IsReachRat", true);
                }
            }
            else
            {
                animator.SetBool("IsKnowWhereRat", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            if (LookLeft)
            {
                if (animator.GetBool("IsKnowWhereRat"))
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x - 0.12f, enemy.transform.position.y);
                }
                else enemy.transform.position = new Vector3(enemy.transform.position.x - 0.075f, enemy.transform.position.y);
            }
            else
            {
                if (animator.GetBool("IsKnowWhereRat"))
                {
                    enemy.transform.position = new Vector3(enemy.transform.position.x + 0.12f, enemy.transform.position.y);
                }
                else enemy.transform.position = new Vector3(enemy.transform.position.x + 0.075f, enemy.transform.position.y);
            }
            if (health == 0)
            {
                IsDead = true;
                animator.SetBool("RatReborn", false);
                animator.SetBool("IsDead", true);
                GetComponent<BoxCollider2D>().enabled = false;
                gameObject.transform.position = new Vector3(-100, -100);
            }
            else health--;
        }
    }
}
