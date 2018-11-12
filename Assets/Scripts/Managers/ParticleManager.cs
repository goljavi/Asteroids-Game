using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
    GameObject _shipHitParticle;
    GameObject _loseParticle;
    Pool<GameObject> _asteroidHitParticlePool;

    void Start () {
        _asteroidHitParticlePool = new Pool<GameObject>(8, AsteroidHitParticleFactory, ActivateParticle, DeactivateParticle, true);
        _shipHitParticle = ParticleFactory((GameObject)Resources.Load(ResourcesNames.SHIPHITPARTICLE));
        _loseParticle = ParticleFactory((GameObject)Resources.Load(ResourcesNames.LOSEPARTICLE));

        _shipHitParticle.SetActive(false);
        _loseParticle.SetActive(false);

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, (p) => AsteroidHitParticle(p));
        EventsManager.SubscribeToEvent(EventType.BOMB_EXPLOSION, (p) => AsteroidHitParticle(p));
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLoseConditionAchieved);
    }

    void AsteroidHitParticle(object[] parameterContainer)
    {
        var particle = _asteroidHitParticlePool.GetObjectFromPool();
        particle.transform.position = (Vector3)parameterContainer[0];
        ReturnToPoolDelay(particle, 1);
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        SetParticleIndividual(_shipHitParticle, (Vector3)parameterContainer[1]);
    }

    void OnLoseConditionAchieved(object[] parameterContainer)
    {
        SetParticleIndividual(_loseParticle, transform.position);
    }

    void SetParticleIndividual(GameObject particle, Vector3 position)
    {
        ActivateParticle(particle);
        particle.transform.position = position;
        StartCoroutine(DeactivateParticleDelay(particle, 1));
    }

    IEnumerator DeactivateParticleDelay(GameObject particle, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeactivateParticle(particle);
    }

    IEnumerator ReturnToPoolDelay(GameObject particle, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _asteroidHitParticlePool.DisablePoolObject(particle);
    }

    void ActivateParticle(GameObject particle)
    {
        particle.SetActive(true);
    }

    void DeactivateParticle(GameObject particle)
    {
        particle.SetActive(false);
    }

    GameObject AsteroidHitParticleFactory()
    {
        return ParticleFactory((GameObject)Resources.Load(ResourcesNames.ASTEROIDHITPARTICLE));
    }

    GameObject ParticleFactory(GameObject factory)
    {
        return Instantiate(factory);
    }
}
