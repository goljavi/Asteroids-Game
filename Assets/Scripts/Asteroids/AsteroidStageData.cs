using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidStageData", menuName = "AsteroidStageData")]
public class AsteroidStageData : ScriptableObject
{
    public int stage;
    public Vector3 size;
    public float thrust;
    public float torque;
}
