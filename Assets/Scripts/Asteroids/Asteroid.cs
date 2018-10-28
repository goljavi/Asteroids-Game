using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    public float yBoundary;
    public float xBoundary;

    float _thrust;
    float _torque;
    int _stage;
    Rigidbody2D _rb;

    void Update () {
        Boundaries();
	}

    void Move()
    {
        _rb.AddForce(new Vector3(Random.Range(-_thrust, _thrust), Random.Range(-_thrust, _thrust), 0));
        _rb.AddTorque(Random.Range(-_torque, _torque));
    }

    public void SetAsteroid(int stage, float thrust, float torque, Vector3 size, Vector3 position)
    {
        _stage = stage;
        _thrust = thrust;
        _torque = torque;
        transform.localScale = size;
        transform.position = position;
        Move();
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

    public void Activate()
    {
        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
    }

    public static void ActivateAsteroid(Asteroid asteroidObj)
    {
        asteroidObj.gameObject.SetActive(true);
        asteroidObj.Activate();
    }

    public static void DeactivateAsteroid(Asteroid asteroidObj)
    {
        asteroidObj.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Kill();
    }

    public void Kill()
    {
        EventsManager.TriggerEvent(EventType.ASTEROID_HIT, transform.position, _stage);
        AsteroidSpawner.Instance.ReturnAsteroidToPool(this);
    }
}
