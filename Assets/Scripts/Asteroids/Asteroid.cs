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
    BoundariesChecker _bc;

    void Start()
    {
        _bc = new BoundariesChecker(transform, xBoundary, yBoundary);
    }

    void Update () {
        _bc.Update();
	}

    void Move()
    {
        _rb.AddForce(-(Vector3.zero - transform.position).normalized * Random.Range(0.5f, _thrust));
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
        EventsManager.TriggerEvent(EventType.ASTEROID_HIT, transform.position, _stage, this);
    }
}
