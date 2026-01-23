using UnityEngine;

public class SoudManager : MonoBehaviour
{
    
    public static SoudManager instance;
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



}
