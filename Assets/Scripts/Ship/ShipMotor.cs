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
    public int totalLife;
    public int asteroidHitScore;
    int _actualLife;
    int _score;
    bool _ableToMove = true;
    ShipController _sc;


    void Start () {
        _score = 0;
        _actualLife = totalLife;
        _sc = new ShipController(this);
        _rb = GetComponent<Rigidbody2D>();
        EventsManager.SubscribeToEvent(EventType.SHOWING_INTERACTIVE_CONTENT, OnInteractiveContentShown);
        EventsManager.SubscribeToEvent(EventType.CLOSED_INTERACTIVE_CONTENT, OnInteractiveContentClosed);
        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);
    }
	
	void Update () {
        Boundaries();
        _sc.Update();
	}

    public void Move(float vertical, float horizontal)
    {
        if (!_ableToMove) return;
        _rb.AddForce(transform.up * thrust * vertical);
        transform.Rotate(Vector3.forward * -horizontal * Time.deltaTime * rotationThrust);
    }

    public void Shoot()
    {
        if (cooldownTimer >= Time.time || !_ableToMove) return;
        cooldownTimer = Time.time + weaponCooldown;
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

    void OnInteractiveContentShown(object[] parameterContainer)
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        _ableToMove = false;
    }

    void OnInteractiveContentClosed(object[] parameterContainer)
    {
        _rb.isKinematic = false;
        _ableToMove = true;
    }

    void OnAsteroidHit(object[] parameterContainer)
    {
        _score += asteroidHitScore;
        EventsManager.TriggerEvent(EventType.SCORE_UPDATED, _score);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude < thresholdForce || !_ableToMove) return;

        _actualLife--;
        EventsManager.TriggerEvent(EventType.SHIP_LIFE_CHANGED, _actualLife);
        Debug.Log("Morí: " + other.relativeVelocity.magnitude);
    }
}
