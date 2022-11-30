using System;
using UnityEngine;

public class MusicTheme : MonoBehaviour
{
    /*
     * MUSIC HANDLER
     * 
     * Singleton, to continue music loop between scenes
     */


    public static MusicTheme _instance;

    [SerializeField]
    private Sound[] musics;
    private bool DontPlayMusicAtStart => GodModeManager.Instance.DontPlayMusicAtStart;
    private string theme = "Theme";

    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in musics)
        {
            s.aSource = gameObject.AddComponent<AudioSource>();
            s.aSource.clip = s.clip;
            s.aSource.volume = s.volume;
            s.aSource.pitch = s.pitch;
            s.aSource.loop = s.loop;
        }
    }


    void Start()
    {
        if (!DontPlayMusicAtStart)
        {
            Play(theme);
        }
        GameObject.FindWithTag("Player").GetComponent<Player>().gameOver += Stop;
    }


    public void Play(string name)
    {
        Sound s = Array.Find(musics, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " note found!");
            return;
        }

        s.aSource.Play();
    }

    private void Stop()
    {
        Sound s = Array.Find(musics, sound => sound.name == theme);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " note found!");
            return;
        }
        s.aSource.Stop();
    }

}
