using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Text _livesText;
    Text _scoreText;
    GameObject _startMenu;
    GameObject _pauseMenu;
    GameObject _winMenu;
    GameObject _loseMenu;
    UIController _uic;

    void Start() {
        _livesText = (Text)Resources.Load(ResourcesNames.LIVES, typeof(Text));
        _scoreText = (Text)Resources.Load(ResourcesNames.SCORE, typeof(Text));
        _startMenu = (GameObject)Resources.Load(ResourcesNames.STARTMENU, typeof(GameObject));
        _pauseMenu = (GameObject)Resources.Load(ResourcesNames.PAUSEMENU, typeof(GameObject));
        _winMenu = (GameObject)Resources.Load(ResourcesNames.WINMENU, typeof(GameObject));
        _loseMenu = (GameObject)Resources.Load(ResourcesNames.LOSEMENU, typeof(GameObject));
        InstantiateUI();

        _uic = new UIController(this);
        SetEvents(); 
    }

    void SetEvents()
    {
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, OnScoreUpdate);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLose);
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, OnWin);
    }

    void InstantiateUI()
    {
        var canvas = GameObject.Find("Canvas").transform;
        Instantiate(_livesText).transform.SetParent(canvas, false);
        Instantiate(_scoreText).transform.SetParent(canvas, false);
        Instantiate(_startMenu).transform.SetParent(canvas, false);
        Instantiate(_pauseMenu).transform.SetParent(canvas, false);
        Instantiate(_winMenu).transform.SetParent(canvas, false);
        Instantiate(_loseMenu).transform.SetParent(canvas, false);

        _pauseMenu.SetActive(false);
        _winMenu.SetActive(false);
        _loseMenu.SetActive(false);
        _startMenu.SetActive(false);

        ToggleStartMenu();
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
