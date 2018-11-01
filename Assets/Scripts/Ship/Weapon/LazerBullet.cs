using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBullet : IBulletBehaviour
{
    float _lifeSpan;
    float _tick;
    Bullet _reference;

    public LazerBullet(float lifeSpan, Bullet reference)
    {
        _lifeSpan = lifeSpan;
        _reference = reference;
        EventsManager.TriggerEvent(EventType.LAZER_SHOOT);
    }

    public void Init()
    {
        Stretch();
    }

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
            _reference.ReturnBulletToPool();
        }
    }

    void Stretch()
    {
        _reference.transform.Translate(new Vector3(0, 18, 0));

        var locSc = _reference.transform.localScale;
        _reference.transform.localScale = new Vector3(locSc.x, 200, locSc.z);
    }
}
