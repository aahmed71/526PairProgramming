using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        var direction = new Vector2(x, y);

        rb.velocity = direction * speed;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    void OnMouseDown()
    {
        player.GetComponent<PlayerController>().Grapple();
    }

    private void OnMouseUp()
    {
        player.GetComponent<PlayerController>().ReleaseGrapple();
    }


}
