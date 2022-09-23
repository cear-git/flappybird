using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Score
{
    public static int getHighscore()
    {
        return PlayerPrefs.GetInt("fbhighscore");
    }

    public static bool trySetHighscore(int score)
    {
        int current = getHighscore();
        if (score > current)
        {
            PlayerPrefs.SetInt("fbhighscore", score);
            PlayerPrefs.Save();
            return true;
        }
        return false;

    }

    public static void resetHS()
    {
        PlayerPrefs.SetInt("fbhighscore", 0);
        PlayerPrefs.Save();
    }
}
