﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : IBulletBehaviour {

    float _speed;
    float _lifeSpan;
    float _tick;
    Bullet _reference;

    public NormalBullet(float speed, float lifeSpan, Bullet reference)
    {
        _speed = speed;
        _lifeSpan = lifeSpan;
        _reference = reference;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _reference.ReturnBulletToPool();
    }

    public void Start(){}

    public void Update()
    {
        _tick += Time.deltaTime;
        if (_tick >= _lifeSpan)
        {
            _reference.ReturnBulletToPool();
        }
        else
        {
            _reference.transform.position += _reference.transform.up * _speed * Time.deltaTime;
        }
    }
}
