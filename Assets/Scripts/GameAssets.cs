using System;
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

    public Transform pfPipeHead;
    public Transform pfPipeBody;
    public Transform pfGround;
    public Transform[] pfClouds;

    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip ac;
    }
}
