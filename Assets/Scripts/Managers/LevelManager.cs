using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public int winScore;

    void Start()
    {
        EventsManager.Init();
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, OnShipScoreUpdate);

        new SoundManager(GetComponent<AudioSource>());
        Instantiate((GameObject)Resources.Load(ResourcesNames.SHIP, typeof(GameObject))).transform.position = Vector3.zero;
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        if ((int)parameterContainer[0] < 1) EventsManager.TriggerEvent(EventType.LOSE_CONDITION_ACHIEVED);
    }

    void OnShipScoreUpdate(object[] parameterContainer)
    {
        if ((int)parameterContainer[0] >= winScore) EventsManager.TriggerEvent(EventType.WIN_CONDITION_ACHIEVED);
    }

    public void Restart()
    {
        EventsManager.TriggerEvent(EventType.CLEAR_ALL);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
