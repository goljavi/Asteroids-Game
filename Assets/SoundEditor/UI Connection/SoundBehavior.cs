using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehavior : MonoBehaviour
{
    public SoundNodeMap _soundNodeMap;
    public bool loop;
    public bool startOnPlay;
    public List<AudioClip> _clips;

    List<SoundObject> _soundObjects;
    AudioSource _as;
    int current;
    bool canPlayLoop;

    public bool IsPlaying { get; private set; }

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _soundObjects = new List<SoundObject>();

        var startingNode = GetStartingNode();
        _soundObjects.Add(MakeSoundObject(startingNode));

        var currentNode = GetChild(startingNode);
        while(currentNode != null)
        {

            if(currentNode.nodeType == "Sound")
            {
                _soundObjects.Add(MakeSoundObject(currentNode));
            }
            currentNode = GetChild(currentNode);
        }

        if(startOnPlay) StartPlaying();
        
    }

    void Update()
    {
        if (loop && canPlayLoop) StartPlaying();
    }

    public void StartPlaying()
    {
        if (IsPlaying) return;
        canPlayLoop = true;
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        IsPlaying = true;
        SoundObject soundObject = GetSoundObject(current);
        while (soundObject != null)
        {
            yield return new WaitForSeconds(soundObject.delay);
            _as.clip = _clips[current];
            _as.Play();


            if (soundObject.stop > 0 && soundObject.stop <= _clips[current].length)
            {
                yield return new WaitForSeconds(soundObject.stop);
            }
            else
            {
                yield return new WaitForSeconds(_clips[current].length);
            }

            _as.Stop();
            current++;
            soundObject = GetSoundObject(current);
        }
        if (loop) current = 0;
        IsPlaying = false;
    }


    SoundObject GetSoundObject(int index)
    {
        foreach (var item in _soundObjects)
        {
            if (item.id == current) return item;
        }

        return null;
    }

    SoundObject MakeSoundObject(SoundMapSerializedObject obj)
    {
        if (obj.nodeType != "Sound") return null;

        var data = obj.data.Split('|');

        return new SoundObject()
        {
            id = int.Parse(data[0]),
            stop = float.Parse(data[1]),
            delay = GetDelay(obj)
        };
    }

    float GetDelay(SoundMapSerializedObject obj)
    {
        var parent = GetParent(obj);
        if (parent == null || parent.nodeType != "Delay") return 0;
        return float.Parse(parent.data);
    }

    SoundMapSerializedObject GetParent(SoundMapSerializedObject obj)
    {
        foreach (var node in _soundNodeMap.nodes)
        {
            if(obj.id == node.id)
            {
                foreach (var parent in node.parentIds)
                {
                    foreach (var node2 in _soundNodeMap.nodes)
                    {
                        if (node2.id == parent) return node2;
                    }
                }
            }
        }

        return null;
    }

    SoundMapSerializedObject GetChild(SoundMapSerializedObject obj)
    {
        foreach (var node in _soundNodeMap.nodes)
        {
            if(node.id == obj.id)
            {
                foreach (var node2 in _soundNodeMap.nodes)
                {
                    foreach (var parent in node2.parentIds)
                    {
                        if (node.id == parent) return node2;
                    }
                }
            }
            
        }

        return null;
    }

    SoundMapSerializedObject GetStartingNode()
    {
        foreach (var node in _soundNodeMap.nodes)
        {
            foreach (var parent in node.parentIds)
            {
                foreach (var node2 in _soundNodeMap.nodes)
                {
                    if (node2.id == parent && node2.nodeType == "Start") return node;
                }
            }

        }

        return null;
    }
}