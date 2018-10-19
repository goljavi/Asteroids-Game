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
            ShipWeapon.Instance.ReturnBulletToPool(this);
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void Initialize()
    {
        transform.position = Vector3.zero;
    }

    public static void InitializeBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Initialize();
    }

    public static void DisposeBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(false);
    }
}
