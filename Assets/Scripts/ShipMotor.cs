using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotor : MonoBehaviour {
    Rigidbody2D _rb;
    public float thrust;
    public float rotationThrust;
    public float yBoundary;
    public float xBoundary;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Boundaries();
	}

    public void Move(float vertical, float horizontal)
    {
        _rb.AddForce(transform.up * thrust * vertical);
        _rb.AddTorque(rotationThrust * -horizontal);
    }

    public void Shoot()
    {
        var bullet = ShipWeapon.Instance.GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.up = transform.up;
    }

    void Boundaries()
    {
        var transf = transform.position;
        if (transform.position.y > yBoundary)
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
