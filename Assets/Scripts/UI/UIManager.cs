using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int score;
    public Text livesText;
    public Text scoreText;
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    UIController _uic;

    void Start () {
        _uic = new UIController(this);
        ToggleStartMenu();
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
        livesText.text = "Lives: " + (int)parameterContainer[0];
    }

    void OnScoreUpdate(object[] parameterContainer)
    {
        scoreText.text = "Score: " + (int)parameterContainer[0];
    }

    public void ToggleLoseMenu()
    {
        var active = !loseMenu.activeSelf;
        loseMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void ToggleWinMenu()
    {
        var active = !winMenu.activeSelf;
        winMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void ToggleStartMenu()
    {
        var active = !startMenu.activeSelf;
        startMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void TogglePauseMenu()
    {
        if (startMenu.activeSelf) return;
        var active = !pauseMenu.activeSelf;
        pauseMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

}
