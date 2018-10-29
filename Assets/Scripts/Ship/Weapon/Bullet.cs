using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    IBulletBehaviour bullet;
    public delegate void Behavior();
    Dictionary<string, Behavior> _behaviors;

    public void Update()
    {
        bullet.Update();
    }

    public void SetBehavior(string behavior)
    {
        if (_behaviors.ContainsKey(behavior)) _behaviors[behavior]();
    }

    void SetNormalBehavior()
    {
        bullet = new NormalBullet(5, 5, this);
    }

    void SetLazerBehavior()
    {
        bullet = new LazerBullet(0.3f, this);
    }

    void SetBombBehavior()
    {
        bullet = new BombBullet(5, this);
    }

    public void Activate()
    {
        _behaviors = new Dictionary<string, Behavior>
        {
            { BulletBehavior.Normal, SetNormalBehavior },
            { BulletBehavior.Lazer, SetLazerBehavior },
            { BulletBehavior.Bomb, SetBombBehavior }
        };

        bullet = new NormalBullet(5, 5, this);
        transform.position = Vector3.zero;
    }

    public void Deactivate()
    {
        transform.localScale = new Vector3(1, 1, 1);
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

    public void ReturnBulletToPool()
    {
        ShipWeapon.Instance.ReturnBulletToPool(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bullet.OnTriggerEnter2D(other);
    }
}
