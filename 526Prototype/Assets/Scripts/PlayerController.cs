using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private DistanceJoint2D _distanceJoint;
    [SerializeField] private bool isGrappled = false;
    [SerializeField] private bool isPulling = false;
    public GameObject grappled;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
        _lineRenderer = GetComponent<LineRenderer>();
        _distanceJoint = GetComponent<DistanceJoint2D>();
        _distanceJoint.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }*/
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(0, grappled.transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }
        //If Space is pressed and player is grappled
        if(Input.GetKey("space") && isGrappled)
        {
            isPulling = true;
            //Pulls player towards grappled object
            transform.position = Vector2.MoveTowards(transform.position, grappled.transform.position, Time.deltaTime * 8);
            GrapplePull();  
        }
        //If it is grappled and pull was set to true
        if(isPulling && isGrappled)
        {
            GrapplePull();    
        }
        //If space is released when pulling
        if(Input.GetKeyUp("space") && isPulling)
        {
            //Keeps momentum and releases grapple
            rb.velocity = ((grappled.transform.position - transform.position).normalized) * speed;
            ReleaseGrapple();
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity = rb.velocity.normalized * speed;
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity.normalized * speed, Time.deltaTime * 3f);
    }

    private void GrapplePull()
    {
        rb.velocity *= (grappled.transform.position - transform.position).normalized;
        rb.velocity = Vector2.Lerp(rb.velocity, (grappled.transform.position - transform.position).normalized * speed, Time.deltaTime * 3f);
    }    

    public void Grapple(GameObject gameObj)
    {
        isGrappled = true;
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        grappled = gameObj;
        _lineRenderer.SetPosition(0, gameObj.transform.position);
        _lineRenderer.SetPosition(1, transform.position);
        _distanceJoint.connectedAnchor = gameObj.transform.position;
        _distanceJoint.enabled = true;
        _lineRenderer.enabled = true;
        Debug.Log("tag");
    }

    public void ReleaseGrapple()
    {
        isGrappled = false;
        isPulling = false;
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }
}
