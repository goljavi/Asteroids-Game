using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public string weapon;
    Pool<Bullet> _bulletPool;
    WeaponController _wc;


    private static ShipWeapon _instance;
    public static ShipWeapon Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
        _bulletPool = new Pool<Bullet>(8, BulletFactory, Bullet.ActivateBullet, Bullet.DeactivateBullet, true);
        _wc = new WeaponController(this);

        weapon = BulletBehavior.Normal;
    }

    void Update()
    {
        _wc.Update();
    }

    public Bullet GetBulletFromPool()
    {
        var bullet = _bulletPool.GetObjectFromPool();
        bullet.SetBehavior(weapon);
        return bullet;
    }

    private Bullet BulletFactory()
    {
        return Instantiate<Bullet>(bulletPrefab);
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.DisablePoolObject(bullet);
    }
}
