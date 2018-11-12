using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    public List<AsteroidStageData> asteroidStageDataList;
    public int initialAsteroidStage;

    Dictionary<int, AsteroidStageData> _asteroidStageDataDictionary;
    Asteroid _asteroidPrefab;
    Pool<Asteroid> _asteroidPool;

    void Start()
    {
        _asteroidPrefab = (Asteroid)Resources.Load(ResourcesNames.ASTEROID, typeof(Asteroid));
        _asteroidPool = new Pool<Asteroid>(8, AsteroidFactory, Asteroid.ActivateAsteroid, Asteroid.DeactivateAsteroid, true);
        _asteroidStageDataDictionary = new Dictionary<int, AsteroidStageData>();

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);
        EventsManager.SubscribeToEvent(EventType.GAME_STARTED, OnGameStarted);
    }

    void OnGameStarted(object[] parametersContainer)
    {
        foreach (var item in asteroidStageDataList)
        {
            _asteroidStageDataDictionary[item.stage] = item;
        }

        for (int i = 0; i < 8; i++)
        {
            if (Random.value > 0.5f) Spawn(initialAsteroidStage, new Vector3(Random.Range(18, -18), Random.Range(10, 2), 0));
            else Spawn(initialAsteroidStage, new Vector3(Random.Range(18, -18), Random.Range(-2, -10), 0));
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
        ReturnAsteroidToPool((Asteroid)parametersContainer[2]);
    }

    void Spawn(int stage, Vector3 position)
    {
        if (!_asteroidStageDataDictionary.ContainsKey(stage)) return;
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
        return Instantiate(_asteroidPrefab);
    }

    public void ReturnAsteroidToPool(Asteroid asteroid)
    {
        _asteroidPool.DisablePoolObject(asteroid);
    }
}
