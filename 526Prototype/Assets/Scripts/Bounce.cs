using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float explosionForce = 5;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector2 explodeDirection = (collision.transform.position - transform.position).normalized;
            collision.rigidbody.velocity = explodeDirection * explosionForce;
            collision.transform.GetComponent<PlayerController>().ReleaseGrapple();
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
