using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMotor : MonoBehaviour {
    Rigidbody2D _rb;
    float cooldownTimer;
    public float weaponCooldown;
    public float thrust;
    public float rotationThrust;
    public float yBoundary;
    public float xBoundary;
    public float thresholdForce;


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
        transform.Rotate(Vector3.forward * -horizontal * Time.deltaTime * rotationThrust);
    }

    public void Shoot()
    {
        
        if (cooldownTimer <= Time.time)
        {
            cooldownTimer = Time.time + weaponCooldown;

            var bullet = ShipWeapon.Instance.GetBulletFromPool();
            bullet.transform.position = transform.position;
            bullet.transform.up = transform.up;
        }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.relativeVelocity.magnitude > thresholdForce)
        {
            Debug.Log("Morí: " + other.relativeVelocity.magnitude);
        }
    }
}
