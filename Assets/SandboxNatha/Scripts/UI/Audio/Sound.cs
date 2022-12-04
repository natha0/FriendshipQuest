using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume=.5f;

    [Range(.1f, 3f)]
    public float pitch=1;

    public bool loop;

    [HideInInspector]
    public AudioSource aSource=null;


    public bool specifyMusicParams;
    public int musicBPM, timeSignature, barsLength;

}
