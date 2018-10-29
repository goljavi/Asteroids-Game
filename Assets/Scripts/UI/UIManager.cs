using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text _livesText;
    public Text _scoreText;
    public GameObject _startMenu;
    public GameObject _pauseMenu;
    public GameObject _winMenu;
    public GameObject _loseMenu;
    UIController _uic;

    void Start() {
        _uic = new UIController(this);
        ToggleStartMenu();
        SetEvents(); 
    }

    void SetEvents()
    {
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, OnScoreUpdate);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLose);
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, OnWin);
    }

    void Update () {
        _uic.Update();
	}

    void OnLose(object[] parameterContainer)
    {
        ToggleLoseMenu();
    }

    void OnWin(object[] parameterContainer)
    {
        ToggleWinMenu();
    }

    void OnShipLifeChanged(object[] parameterContainer)
    {
        _livesText.text = "Lives: " + (int)parameterContainer[0];
    }

    void OnScoreUpdate(object[] parameterContainer)
    {
        _scoreText.text = "Score: " + (int)parameterContainer[0];
    }

    public void ToggleLoseMenu()
    {
        var active = !_loseMenu.activeSelf;
        _loseMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void ToggleWinMenu()
    {
        var active = !_winMenu.activeSelf;
        _winMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void ToggleStartMenu()
    {
        var active = !_startMenu.activeSelf;
        _startMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void TogglePauseMenu()
    {
        if (_startMenu.activeSelf) return;
        var active = !_pauseMenu.activeSelf;
        _pauseMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

}
