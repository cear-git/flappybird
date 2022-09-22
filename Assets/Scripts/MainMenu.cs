using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); };
        transform.Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => {  };
    }
}
