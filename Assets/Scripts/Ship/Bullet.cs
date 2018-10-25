using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;
    public float lifeSpan;
    private float _tick;

    void Update()
    {
        _tick += Time.deltaTime;
        if (_tick >= lifeSpan)
        {
            ReturnBulletToPool();
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    void ReturnBulletToPool()
    {
        ShipWeapon.Instance.ReturnBulletToPool(this);
    }

    public void Activate()
    {
        transform.position = Vector3.zero;
    }

    public void Deactivate()
    {
        _tick = 0;
    }

    public static void ActivateBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Activate();
    }

    public static void DeactivateBullet(Bullet bulletObj)
    {
        bulletObj.Deactivate();
        bulletObj.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ReturnBulletToPool();
    }
}
