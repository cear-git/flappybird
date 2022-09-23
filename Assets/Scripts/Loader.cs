using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Loading class mainly for learning how something like this might work
 * It's pretty useless for this game specifically as it takes barely any time to load at all 
 */

public static class Loader
{
    public enum Scene
    {
        GameScene,
        Loading,
        MainMenu
    }

    private static Scene targetScene;
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(Scene.Loading.ToString());
        targetScene = scene;
    }

    public static void loadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
