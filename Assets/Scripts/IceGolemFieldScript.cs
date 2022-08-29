using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGolemFieldScript : MonoBehaviour
{

    public IceGolemScript iceGolemScript;
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
            iceGolemScript.KnowWhereEnemy = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            iceGolemScript.KnowWhereEnemy = false;
        }
    }
}
