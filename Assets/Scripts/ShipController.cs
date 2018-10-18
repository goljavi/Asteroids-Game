using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    Rigidbody2D _rb;
    public float thrust;
    public float rotationThrust;
    public float yBoundary;
    public float xBoundary;

	void Start () {
        _rb = GetComponent<Rigidbody2D>();	
	}
	
	void Update () {
        _rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
        _rb.AddTorque(rotationThrust * -Input.GetAxis("Horizontal"));

        var transf = transform.position;
        if(transform.position.y > yBoundary)
        {
            transf.y = -yBoundary;
            transform.position = transf;
        }
        else if (transform.position.y < -yBoundary)
        {
            transf.y = yBoundary;
            transform.position = transf;
        }
        else if (transform.position.x > xBoundary)
        {
            transf.x = -xBoundary;
            transform.position = transf;
        }
        else if (transform.position.x < -xBoundary)
        {
            transf.x = xBoundary;
            transform.position = transf;
        }
    }
}
