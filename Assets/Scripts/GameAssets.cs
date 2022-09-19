using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets getInstance() 
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public Sprite pipeHead;
    public Sprite pipeBody;
    public Sprite ground;
}
