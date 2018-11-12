using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float normalBulletSpeed;
    public float normalBulletLifeSpan;
    public float lazerBulletLifeSpan;
    public float bombBulletLifeSpan;
    public LayerMask bombLayerMask;
    IBulletBehaviour _bullet;
    Dictionary<string, IBulletBehaviour> _behaviors;

    public void Update()
    {
        _bullet.Update();
    }

    public void SetBehavior(string behavior)
    {
        if (!_behaviors.ContainsKey(behavior)) return;
        _bullet = _behaviors[behavior];
        _bullet.Reset();
    }

    public void Init()
    {
        _bullet.Init();
    }

    public void Activate()
    {
        _behaviors = new Dictionary<string, IBulletBehaviour>
        {
            { BulletBehavior.Normal, new NormalBullet(normalBulletSpeed, normalBulletLifeSpan, this) },
            { BulletBehavior.Lazer, new LazerBullet(lazerBulletLifeSpan, this) },
            { BulletBehavior.Bomb, new BombBullet(bombBulletLifeSpan, this, bombLayerMask) }
        };

        _bullet = _behaviors[BulletBehavior.Normal];
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
        EventsManager.TriggerEvent(EventType.RETURN_BULLET, this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _bullet.OnTriggerEnter2D(other);
    }
}
