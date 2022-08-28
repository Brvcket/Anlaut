using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonField : MonoBehaviour
{

    public SkeletonScript skeleton_script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skeleton_script.KnowWhereEnemy = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skeleton_script.KnowWhereEnemy = false;
        }
    }
}
