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

    // Use this for initialization
    void Start () {
        startMenu.SetActive(true);
        EventsManager.TriggerEvent(EventType.SHOWING_INTERACTIVE_CONTENT);
        EventsManager.SubscribeToEvent(EventType.ASTEROID_HIT, AsteroidHitListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score;
    }

    public void AsteroidHitListener(object[] parameterContainer)
    {
        UpdateScore(20);
    }

}
