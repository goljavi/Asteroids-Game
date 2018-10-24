using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public Asteroid asteroidPrefab;
    private Pool<Asteroid> _asteroidPool;

    private static AsteroidSpawner _instance;
    public static AsteroidSpawner Instance { get { return _instance; } }

    void Start()
    {
        _instance = this;
        _asteroidPool = new Pool<Asteroid>(8, AsteroidFactory, Asteroid.ActivateAsteroid, Asteroid.DeactivateAsteroid, true);
        for (int i = 0; i < 8; i++)
        {
            var asteroid = _asteroidPool.GetObjectFromPool();
            asteroid.transform.position = new Vector3(Random.Range(18, -18), Random.Range(10, -10), 0);
        }
    }

    public void InstantiateDefined(int stage, Vector3 position)
    {
        var asteroid = GetAsteroidFromPool();
        asteroid.SetStage(stage);
        asteroid.transform.position = new Vector3(position.x + Random.Range(-3, 3), position.y + Random.Range(-3, 3), 0);
    }

    public Asteroid GetAsteroidFromPool()
    {
        return _asteroidPool.GetObjectFromPool();
    }

    private Asteroid AsteroidFactory()
    {
        return Instantiate(asteroidPrefab);
    }

    public void ReturnAsteroidToPool(Asteroid asteroid)
    {
        _asteroidPool.DisablePoolObject(asteroid);
    }
}
