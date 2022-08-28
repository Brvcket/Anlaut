using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public float LaserSpeed = 5f;
    public Rigidbody2D LaserRB;

    // Start is called before the first frame update
    void Start()
    {
        LaserRB.velocity = transform.right * LaserSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }
}
