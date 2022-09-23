using CodeMonkey.Utils;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Jump,
        Score,
        Lose
    }
    public static void Play(Sound sound)
    {
        GameObject go = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = go.GetComponent<AudioSource>();
        audioSource.PlayOneShot(getAudioClip(sound));
    }

    public static AudioClip getAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAC in GameAssets.getInstance().soundAudioClipArray)
        {
            if (soundAC.sound == sound)
            {
                return soundAC.ac;
            }
        }
        Debug.LogError($"Sound {sound} not found");
        return null;
    }
}
