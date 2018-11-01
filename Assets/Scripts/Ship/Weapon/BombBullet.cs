using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : IBulletBehaviour
{
    float _lifeSpan;
    float _tick;
    Bullet _reference;
    LayerMask _lm;

    public BombBullet(float lifeSpan, Bullet reference, LayerMask lm)
    {
        _lifeSpan = lifeSpan;
        _reference = reference;
        _lm = lm;
    }

    public void Init(){ }

    public void OnTriggerEnter2D(Collider2D other){ }

    public void Reset()
    {
        _tick = 0;
    }

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
        var circle = Physics2D.OverlapCircleAll(_reference.transform.position, 5, _lm);
        foreach (var item in circle)
        {
            item.gameObject.GetComponent<Asteroid>().Kill();
        }
    }
}
