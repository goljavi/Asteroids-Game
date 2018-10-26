using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public Asteroid asteroidPrefab;
    public List<AsteroidStageData> asteroidStageDataList;
    Dictionary<int, AsteroidStageData> _asteroidStageDataDictionary;
    public int initialAsteroidStage;
    private Pool<Asteroid> _asteroidPool;

    private static AsteroidSpawner _instance;
    public static AsteroidSpawner Instance { get { return _instance; } }

    void Start()
    {
        _instance = this;
        _asteroidPool = new Pool<Asteroid>(8, AsteroidFactory, Asteroid.ActivateAsteroid, Asteroid.DeactivateAsteroid, true);
        _asteroidStageDataDictionary = new Dictionary<int, AsteroidStageData>();

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);

        PrepareAsteroidDataDictionary();

        for (int i = 0; i < 8; i++)
        {
            Spawn(initialAsteroidStage, new Vector3(Random.Range(18, -18), Random.Range(10, -10), 0));
        }
        
    }

    void OnAsteroidHit(object[] parametersContainer)
    {
        var position = (Vector3)parametersContainer[0];
        for (int i = 0; i < 2; i++)
        {
            position.x *= Random.Range(0.5f, 1.5f);
            position.y *= Random.Range(0.5f, 1.5f);
            Spawn((int)parametersContainer[1] - 1, position);
        }
    }

    void Spawn(int stage, Vector3 position)
    {
        var data = _asteroidStageDataDictionary[stage];
        var asteroid = _asteroidPool.GetObjectFromPool();
        asteroid.SetAsteroid(data.stage, data.thrust, data.torque, data.size, position);
    }

    public Asteroid GetAsteroidFromPool()
    {
        return _asteroidPool.GetObjectFromPool();
    }

    Asteroid AsteroidFactory()
    {
        return Instantiate(asteroidPrefab);
    }

    void PrepareAsteroidDataDictionary()
    {
        foreach (var item in asteroidStageDataList)
        {
            _asteroidStageDataDictionary[item.stage] = item;
        }
    }

    public void ReturnAsteroidToPool(Asteroid asteroid)
    {
        _asteroidPool.DisablePoolObject(asteroid);
    }
}
