using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour {
    List<string> list;

	void Start () {
        list = new List<string>()
        {
            ResourcesNames.UIMANAGER,
            ResourcesNames.ASTEROIDSPAWNER,
            ResourcesNames.CONDITIONMANAGER,
            ResourcesNames.PARTICLEMANAGER,
            ResourcesNames.LEVELMANAGER
        };

        Spawn();
    }

    void Spawn()
    {
        var managerSpawner = GameObject.Find("ManagerSpawner").transform;
        foreach (var item in list) Instantiate((GameObject)Resources.Load(item, typeof(GameObject))).transform.SetParent(managerSpawner, false);
    }
}
