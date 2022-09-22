using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOver : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.GameScene); };
        transform.Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => { Application.Quit(); };
        scoreText = transform.Find("scoreText").GetComponent<Text>();
    }

    private void Start()
    {
        Bird.getInstance().onDied += Bird_onDied;
        Hide();
    }

    private void Bird_onDied(object sender, System.EventArgs e)
    {
        scoreText.text = Level.getInstance().getPipesPassed().ToString();
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
