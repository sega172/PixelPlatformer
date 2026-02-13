using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;
    public AudioSource sfx;

    private void Awake()
    {
        instance = this;
    }

    public static void PlaySfx(AudioClip clip)
    {
        instance.sfx.PlayOneShot(clip);
    }

    public static void PlaySfx(AudioClip clip, float volumeScale)
    {
        instance.sfx.PlayOneShot(clip, volumeScale);
    }

    public static void PlaySfx(List<AudioClip> clips)
    {
        if (clips==null || clips.Count == 0)
        {
            Debug.Log("Звуки не инициализированы");
            return;
        }

        int i = Random.Range(0, clips.Count);
        instance.sfx.PlayOneShot(clips[i]);
    }



}
