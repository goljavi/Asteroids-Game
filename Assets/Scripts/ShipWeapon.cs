using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Pool<Bullet> _bulletPool;

    private static ShipWeapon _instance;
    public static ShipWeapon Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
        _bulletPool = new Pool<Bullet>(8, BulletFactory, Bullet.InitializeBullet, Bullet.DisposeBullet, true);
    }

    public Bullet GetBulletFromPool()
    {
        return _bulletPool.GetObjectFromPool();
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
