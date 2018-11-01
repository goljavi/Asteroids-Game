using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text[] localizableTexts;
    public UserLanguage language;
    public Text _livesText;
    public Text _scoreText;
    public GameObject _startMenu;
    public GameObject _pauseMenu;
    public GameObject _winMenu;
    public GameObject _loseMenu;
    UIController _uic;
    LocalizationManager _loc;
    string _lang;

    void Start() {
        SetEvents();
        SetLocalization();
        ToggleStartMenu();
        ChangeLivesText(3);
        ChangeScoreText(0);
    }
    void SetLocalization() {

        _loc = new LocalizationManager();

        switch (language)
        {
            case UserLanguage.Spanish:
                _lang = LocalizationLanguages.SPANISH;
                break;
            case UserLanguage.Portuguese:
                _lang = LocalizationLanguages.PORTUGESE;
                break;
            case UserLanguage.English: default:
                _lang = LocalizationLanguages.ENGLISH;
                break;
        }

        foreach (var item in localizableTexts)
        {
            item.text = _loc.GetText(_lang, item.gameObject.name);
        }
    }

    void SetEvents()
    {
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, OnShipLifeChanged);
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, OnScoreUpdate);
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, OnLose);
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, OnWin);
    }

    void Update () {
        if(_uic != null) _uic.Update();
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
        ChangeLivesText((int)parameterContainer[0]);
    }

    void OnScoreUpdate(object[] parameterContainer)
    {
        ChangeScoreText((int)parameterContainer[0]);
    }

    void ChangeLivesText(int lives)
    {
        _livesText.text = _loc.GetText(_lang, _livesText.gameObject.name) + ": " + lives;
    }

    void ChangeScoreText(int score)
    {
        _scoreText.text = _loc.GetText(_lang, _scoreText.gameObject.name) + ": " + score;
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
        var active = !_pauseMenu.activeSelf;
        _pauseMenu.SetActive(active);
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public void StartGame()
    {
        _uic = new UIController(this);
        ToggleStartMenu();
    }

    public enum UserLanguage
    {
        English,
        Spanish,
        Portuguese
    }

}
