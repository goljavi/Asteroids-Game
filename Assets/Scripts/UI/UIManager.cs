using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text[] localizableTexts;
    public Text livesText;
    public Text scoreText;
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    UIController _uic;
    LocalizationManager _loc;
    string _lang;
    int _actualLang;
    int _lives;
    int _score;
    bool _showingInteractiveContent;

    void Start() {
        _loc = new LocalizationManager();
        _uic = new UIController(this);
        EventsManager.SubscribeToEvent(EventType.SHIP_LIFE_CHANGED, (p) => ChangeLivesText((int)p[0]));
        EventsManager.SubscribeToEvent(EventType.SCORE_UPDATED, (p) => ChangeScoreText((int)p[0]));
        EventsManager.SubscribeToEvent(EventType.LOSE_CONDITION_ACHIEVED, (p) => ToggleLoseMenu());
        EventsManager.SubscribeToEvent(EventType.WIN_CONDITION_ACHIEVED, (p) => ToggleWinMenu());

        SetLocalization();
        ToggleStartMenu();
        ChangeLivesText(3);
        ChangeScoreText(0);
    }
    void SetLocalization() {
        switch (_actualLang)
        {
            case 0:
                _lang = LocalizationLanguages.SPANISH;
                break;
            case 1:
                _lang = LocalizationLanguages.PORTUGESE;
                break;
            case 2: default:
                _lang = LocalizationLanguages.ENGLISH;
                break;
        }

        foreach (var item in localizableTexts)
        {
            item.text = _loc.GetText(_lang, item.gameObject.name);
        }
    }

    public void ChangeLocalization()
    {
        _actualLang++;
        if (_actualLang >= System.Enum.GetNames(typeof(UserLanguage)).Length) _actualLang = 0;
        SetLocalization();
        ChangeLivesText(_lives);
        ChangeScoreText(_score);
    }

    void Update () {
        if(_uic != null) _uic.Update();
	}

    void ChangeLivesText(int lives)
    {
        _lives = lives;
        livesText.text = _loc.GetText(_lang, livesText.gameObject.name) + ": " + _lives;
    }

    void ChangeScoreText(int score)
    {
        _score = score;
        scoreText.text = _loc.GetText(_lang, scoreText.gameObject.name) + ": " + _score;
    }

    public void ToggleLoseMenu()
    {
        loseMenu.SetActive(true);
        pauseMenu.SetActive(false);
        TriggerInteractiveContentEvents(true);
    }

    public void ToggleWinMenu()
    {
        winMenu.SetActive(true);
        pauseMenu.SetActive(false);
        TriggerInteractiveContentEvents(true);
    }

    public void ToggleStartMenu()
    {
        var active = !startMenu.activeSelf;
        startMenu.SetActive(active);
        TriggerInteractiveContentEvents(active);

        if (!active)
        {
            EventsManager.TriggerEvent(EventType.GAME_STARTED);
            scoreText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(true);
        }
    }

    public void TogglePauseMenu()
    {
        var active = !pauseMenu.activeSelf;
        if (active && _showingInteractiveContent) return;

        pauseMenu.SetActive(active);
        TriggerInteractiveContentEvents(active);
    }

    void TriggerInteractiveContentEvents(bool active)
    {
        _showingInteractiveContent = active;
        if (active) EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        else EventsManager.TriggerEvent(EventType.CLOSED_INTERACTIVE_CONTENT);
    }

    public enum UserLanguage
    {
        English,
        Spanish,
        Portuguese
    }

}
