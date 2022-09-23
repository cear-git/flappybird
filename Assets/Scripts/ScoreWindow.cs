using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
    }

    private void Start()
    {
        Bird.getInstance().onDied += Bird_onDied;
        Bird.getInstance().onStart += Bird_onStart;
    }

    private void Bird_onDied(object sender, EventArgs e)
    {
        scoreText.enabled = false;
    }

    private void Bird_onStart(object sender, EventArgs e)
    {
        scoreText.enabled = true;
    }

    private void Update()
    {
        scoreText.text = Level.getInstance().getPipesPassed().ToString();
    }
}
