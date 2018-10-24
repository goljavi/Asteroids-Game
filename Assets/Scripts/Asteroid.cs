using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float thrust;
    public float torque;
    public float yBoundary;
    public float xBoundary;
    Rigidbody2D _rb;
    int stage = 3;

    void Update () {
        Boundaries();
	}

    void Move()
    {
        _rb.AddForce(new Vector3(Random.Range(-thrust, thrust), Random.Range(-thrust, thrust), 0));
        _rb.AddTorque(Random.Range(-torque, torque));
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

    void ReturnAsteroidToPool()
    {
        AsteroidSpawner.Instance.ReturnAsteroidToPool(this);
    }

    public void Activate()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
        Move();
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
        SetStage(stage - 1, true);
        EventsManager.TriggerEvent(EventType.ASTEROID_HIT);
    }

    public void SetStage(int st, bool instantiate = false)
    {
        stage = st;
        if (stage == 2)
        {
            this.transform.localScale = new Vector3(2, 2, 2);
            if(instantiate) AsteroidSpawner.Instance.InstantiateDefined(2, transform.position);
        }
        else if (stage == 1)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            if (instantiate) AsteroidSpawner.Instance.InstantiateDefined(1, transform.position);
        }
        else if (stage == 0)
        {
            ReturnAsteroidToPool();
        }
    }
}
