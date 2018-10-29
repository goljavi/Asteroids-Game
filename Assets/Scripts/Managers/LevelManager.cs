using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    GameObject _shipPrefab;

    void Start()
    {
        _shipPrefab = (GameObject)Resources.Load(ResourcesNames.SHIP, typeof(GameObject));
        PrepareLevel();
    }

    void PrepareLevel()
    {
        ShipFactory().transform.position = Vector3.zero;
    }

    GameObject ShipFactory()
    {
        return Instantiate(_shipPrefab);
    }

	public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
