using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : IBulletBehaviour
{
    float _lifeSpan;
    float _tick;
    Bullet _reference;

    public BombBullet(float lifeSpan, Bullet reference)
    {
        _lifeSpan = lifeSpan;
        _reference = reference;
    }

    public void OnTriggerEnter2D(Collider2D other){ }

    public void Update()
    {
        _tick += Time.deltaTime;
        if (_tick >= _lifeSpan)
        {
            Boom();
            _reference.ReturnBulletToPool();
        }
    }

    void Boom()
    {
        EventsManager.TriggerEvent(EventType.BOMB_EXPLOSION, _reference.transform.position);
        var circle = Physics2D.OverlapCircleAll(_reference.transform.position, 5);
        foreach (var item in circle)
        {
            var go = item.gameObject;
            if (go.layer == 9) go.GetComponent<Asteroid>().Kill();
        }
    }
}
