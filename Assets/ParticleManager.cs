using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public GameObject loseParticle;
    public GameObject asteroidHitParticle;
    public GameObject shipHitParticle;

    void Start () {
        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLoseConditionAchieved);
    }
	
	void OnAsteroidHit(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(asteroidHitParticle), (Vector3)parameterContainer[0]);
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(shipHitParticle), (Vector3)parameterContainer[1]);
    }

    void OnLoseConditionAchieved(object[] parameterContainer)
    {
        SetParticle(ParticleFactory(loseParticle), transform.position);
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
