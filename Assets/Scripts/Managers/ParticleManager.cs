using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    Dictionary<string, GameObject> _particleList;

    void Start () {
        _particleList = new Dictionary<string, GameObject>()
        {
            { ResourcesNames.LOSEPARTICLE, (GameObject)Resources.Load(ResourcesNames.LOSEPARTICLE, typeof(GameObject)) },
            { ResourcesNames.ASTEROIDHITPARTICLE, (GameObject)Resources.Load(ResourcesNames.ASTEROIDHITPARTICLE, typeof(GameObject)) },
            { ResourcesNames.SHIPHITPARTICLE, (GameObject)Resources.Load(ResourcesNames.SHIPHITPARTICLE, typeof(GameObject)) }
        };

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLoseConditionAchieved);
        EventsManager.SubscribeToEvent(EventType.BOMB_EXPLOSION, OnBombExplosion);
    }


    void OnBombExplosion(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(_particleList[ResourcesNames.ASTEROIDHITPARTICLE]), (Vector3)parameterContainer[0]);
    }

    void OnAsteroidHit(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(_particleList[ResourcesNames.ASTEROIDHITPARTICLE]), (Vector3)parameterContainer[0]);
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(_particleList[ResourcesNames.SHIPHITPARTICLE]), (Vector3)parameterContainer[1]);
    }

    void OnLoseConditionAchieved(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(_particleList[ResourcesNames.LOSEPARTICLE]), transform.position);
    }

    void SetParticle(GameObject particle, Vector3 position)
    {
        particle.transform.position = position;
        Destroy(particle, 1);
    }

    GameObject ParticleFactory(GameObject factory)
    {
        return Instantiate(factory);
    }
}
