using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOver : MonoBehaviour
{
    private Text highscoreText;
    private Text scoreText;

    private void Awake()
    {
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.GameScene); }; 
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.MainMenu); };
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highscoreText = transform.Find("highscoreText").GetComponent<Text>();
    }

    private void Start()
    {
        Bird.getInstance().onDied += Bird_onDied;
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Loader.Load(Loader.Scene.GameScene);
        }
    }
    private void Bird_onDied(object sender, System.EventArgs e)
    {
        scoreText.text = Level.getInstance().getPipesPassed().ToString();
        if (Score.trySetHighscore(Level.getInstance().getPipesPassed()))
        {
            highscoreText.text = "New Highscore!";
        }
        else { highscoreText.text = $"Highscore: {Score.getHighscore()}"; }
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
