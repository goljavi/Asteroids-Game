using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    public string weapon;

    Bullet _bulletPrefab;
    Pool<Bullet> _bulletPool;
    WeaponController _wc;

    void Awake()
    {
        _bulletPrefab = (Bullet)Resources.Load(ResourcesNames.BULLET, typeof(Bullet));
        _bulletPool = new Pool<Bullet>(8, BulletFactory, Bullet.ActivateBullet, Bullet.DeactivateBullet, true);
        _wc = new WeaponController(this);
        weapon = BulletBehavior.Normal;

        EventsManager.SubscribeToEvent(EventType.SHIP_SHOOT, OnShipShoot);
        EventsManager.SubscribeToEvent(EventType.RETURN_BULLET, OnReturnBullet);
    }

    void Update()
    {
        _wc.Update();
    }

    void OnReturnBullet(object[] parametersContainer)
    {
        ReturnBulletToPool((Bullet)parametersContainer[0]);
    }

    void OnShipShoot(object[] parametersContainer)
    {
        var newTransform = (Transform)parametersContainer[0];
        var bullet = GetBulletFromPool();
        bullet.transform.position = newTransform.position;
        bullet.transform.up = newTransform.up;
        bullet.Init();
    }

    public Bullet GetBulletFromPool()
    {
        var bullet = _bulletPool.GetObjectFromPool();
        bullet.SetBehavior(weapon);
        return bullet;
    }

    private Bullet BulletFactory()
    {
        return Instantiate<Bullet>(_bulletPrefab);
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.DisablePoolObject(bullet);
    }
}
