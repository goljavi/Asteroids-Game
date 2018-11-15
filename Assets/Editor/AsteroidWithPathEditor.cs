using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AsteroidWithPath))]
public class AsteroidWithPathEditor : Editor {
    AsteroidWithPath _target;

    void OnEnable()
    {
        _target = (AsteroidWithPath)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Nodes:");
        for (int i = 0; i < _target.nodes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Vector2Field(i.ToString(), _target.nodes[i]);
            if (GUILayout.Button("Remove"))
            {
                _target.nodes.Remove(_target.nodes[i]);
            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Node"))
        {
            _target.nodes.Add(_target.transform.position);
        }


    }

    void OnSceneGUI()
    {
        Handles.color = Color.white;
        Handles.Label(_target.transform.position + new Vector3(0, 2, 0), _target.name + " - Speed: " + _target.speed + " - Vision: " + _target.radius);
        _target.radius = Handles.RadiusHandle(Quaternion.identity, _target.transform.position, _target.radius);
        _target.speed = Handles.ScaleValueHandle(_target.speed, _target.transform.position, Quaternion.Euler(new Vector3(0,90,0)), _target.speed * 1.5f, Handles.ArrowHandleCap, 0.5f);
        if (_target.speed < 1) _target.speed = 1;

        Handles.color = Color.green;
        for (int i = 0; i < _target.nodes.Count; i++)
        {
            _target.nodes[i] = Handles.PositionHandle(_target.nodes[i], Quaternion.identity);
            Handles.DrawSphere(i, _target.nodes[i], Quaternion.identity, 0.3f);
            Handles.Label(_target.nodes[i], i.ToString());
        }

        for (int i = 0; i < _target.nodes.Count; i++)
        {
            Handles.DrawLine(_target.nodes[i], _target.nodes[(int)Mathf.Repeat(i + 1, _target.nodes.Count)]);
        }
    }
}
