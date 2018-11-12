using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {
    Dictionary<string, AudioClip> _audioList;
    AudioSource _as;

    public SoundManager(AudioSource audioSource)
    {
        _as = audioSource;
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

        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, (p) => Play(_audioList[ResourcesNames.EXPLOSION]));
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, (p) => Play(_audioList[ResourcesNames.CRASH]));
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, (p) => Play(_audioList[ResourcesNames.LOSE]));
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, (p) => Play(_audioList[ResourcesNames.WIN]));
        EventsManager.SubscribeToEvent(EventType.BOMB_EXPLOSION, (p) => Play(_audioList[ResourcesNames.BOMBEXPLOSION]));
        EventsManager.SubscribeToEvent(EventType.CLOSED_INTERACTIVE_CONTENT, (p) => Play(_audioList[ResourcesNames.INTERACTION]));
        EventsManager.SubscribeToEvent(EventType.BULLET_SHOOT, (p) => Play(_audioList[ResourcesNames.SHOOT]));
        EventsManager.SubscribeToEvent(EventType.BULLET_SHOOT, (p) => Play(_audioList[ResourcesNames.LAZER]));
    }

    void Play(AudioClip audio)
    {
        _as.clip = audio;
        _as.Play();
    }
}
