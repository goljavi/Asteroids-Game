using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    Dictionary<string, AudioClip> _audioList;
    AudioSource _as;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _audioList = new Dictionary<string, AudioClip>()
        {
            { ResourcesNames.BOMBEXPLOSION, (AudioClip)Resources.Load(ResourcesNames.BOMBEXPLOSION, typeof(AudioClip)) },
            { ResourcesNames.EXPLOSION, (AudioClip)Resources.Load(ResourcesNames.EXPLOSION, typeof(AudioClip)) },
            { ResourcesNames.LOSE, (AudioClip)Resources.Load(ResourcesNames.LOSE, typeof(AudioClip)) },
            { ResourcesNames.INTERACTION, (AudioClip)Resources.Load(ResourcesNames.INTERACTION, typeof(AudioClip)) },
            { ResourcesNames.LAZER, (AudioClip)Resources.Load(ResourcesNames.LAZER, typeof(AudioClip)) },
            { ResourcesNames.SHOOT, (AudioClip)Resources.Load(ResourcesNames.SHOOT, typeof(AudioClip)) },
            { ResourcesNames.WIN, (AudioClip)Resources.Load(ResourcesNames.WIN, typeof(AudioClip)) },
            { ResourcesNames.CRASH, (AudioClip)Resources.Load(ResourcesNames.CRASH, typeof(AudioClip)) }
        };

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, OnAsteroidHit);
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLoseConditionAchieved);
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, OnWinConditionAchieved);
        EventsManager.SubscribeToEvent(EventType.BOMB_EXPLOSION, OnBombExplosion);
        EventsManager.SubscribeToEvent(EventType.CLOSED_INTERACTIVE_CONTENT, OnClosedInteractiveContent);
        EventsManager.SubscribeToEvent(EventType.BULLET_SHOOT, OnBulletShoot);
        EventsManager.SubscribeToEvent(EventType.BULLET_SHOOT, OnLazerShoot);
    }

    void OnLazerShoot(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.LAZER]);
    }

    void OnBulletShoot(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.SHOOT]);
    }

    void OnClosedInteractiveContent(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.INTERACTION]);
    }

    void OnBombExplosion(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.BOMBEXPLOSION]);
    }

    void OnAsteroidHit(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.EXPLOSION]);
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.CRASH]);
    }

    void OnLoseConditionAchieved(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.LOSE]);
    }

    void OnWinConditionAchieved(object[] parameterContainer)
    {
        Play(_audioList[ResourcesNames.WIN]);
    }

    void Play(AudioClip audio)
    {
        _as.clip = audio;
        _as.Play();
    }
}
