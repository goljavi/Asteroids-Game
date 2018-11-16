using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWithPath : MonoBehaviour {

    public float speed = 5;
    public float radius = 5;
    public List<Vector2> nodes;
    float _threshold = 0.3f;
    int _actualNode;
    GameObject _player;

	void Start () {
        if (nodes.Count > 0) transform.position = nodes[0];
        _actualNode = 1;
        EventsManager.SubscribeToEvent(EventType.SHIP_SPAWNED, OnShipSpawn);
	}

    void OnShipSpawn(object[] parametersContainer)
    {
        var cast = (ShipMotor)parametersContainer[0];
        _player = cast.gameObject;
    }

    void Update () {
        if (nodes.Count == 0 || _player == null) return;
        
        if(Vector3.Distance(transform.position, _player.transform.position) < radius)
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, Time.deltaTime * (speed / 4));
            return;
        }

        if (Vector3.Distance(transform.position, nodes[_actualNode]) < _threshold) _actualNode = (int)Mathf.Repeat(_actualNode + 1, nodes.Count);
        transform.position = Vector3.Lerp(transform.position, nodes[_actualNode], Time.deltaTime * speed);
    }
}
