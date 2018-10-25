using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour {
    public int winScore;

	void Start () {
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, OnShipScoreUpdate);
    }
	
	void OnShipLifeChanged(object[] parameterContainer)
    {
        if((int)parameterContainer[0] < 1) EventsManager.TriggerEvent(EventType.LOSE_CONDITION_ACHIEVED);
    }

    void OnShipScoreUpdate(object[] parameterContainer)
    {
        if ((int)parameterContainer[0] >= winScore) EventsManager.TriggerEvent(EventType.WIN_CONDITION_ACHIEVED);
    }
}
