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
    private readonly string theme = "Theme";
    private Sound currentMusic;
    private string playType;

    // Loop params
    private AudioSource[] loopSources = new AudioSource[2];
    public bool specifyMusicParams;
    public int musicBPM, timeSignature, barsLength;

    public float switchCheckPeriod = 10;

    private double loopPointMinutes, loopPointSeconds;
    private double time;
    private int nextSource;



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

        loopSources[0] = gameObject.AddComponent<AudioSource>();
        loopSources[1] = gameObject.AddComponent<AudioSource>();

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
        Stop();
        Sound s = Array.Find(musics, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " note found!");
            return;
        }
        s.aSource.Play();
        time = AudioSettings.dspTime;
        currentMusic = s;
        playType = "Sound";
    }

    public void PlayAtEnd()
    {
        Sound s = Array.Find(musics, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " note found!");
            return;
        }
        double duration = (double)s.clip.samples / s.clip.frequency;
        s.aSource.PlayScheduled(time + duration) ;
        time += duration;
        currentMusic = s;
        playType = "Sound";
    }

    public void Stop()
    {
        if (playType == "Sound" && currentMusic != null)
        {
            currentMusic.aSource.Stop();
            playType = null;
        }
        else if (playType == "loop")
        {
            CancelInvoke(nameof(SwitchSource));
            loopSources[0].Stop();
            loopSources[1].Stop();
        }
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(musics, sound => sound.name == name);
        SetupLoop(s);

        time = AudioSettings.dspTime;
        loopSources[0].Play();
        nextSource = 1;

        InvokeRepeating(nameof(SwitchSource), (float)loopPointSeconds - 2, switchCheckPeriod);
        playType = "Loop";
    }

    private void SwitchSource()
    {
        if (!loopSources[nextSource].isPlaying)
        {
            loopSources[nextSource].PlayScheduled(time + loopPointSeconds);
            time += loopPointSeconds;
            nextSource = 1 - nextSource;
        }
    }

    private void SetupLoop(Sound s)
    {
        for (int i = 0; i < loopSources.Length; i++)
        {
            AudioSource source = loopSources[i];
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
        }

        if (s.specifyMusicParams)
        {
            musicBPM = s.musicBPM;
            timeSignature = s.timeSignature;
            barsLength = s.barsLength;

            loopPointMinutes = (double)(barsLength * timeSignature) / musicBPM;
            loopPointSeconds = loopPointMinutes * 60d;
        }
        else
        {
            loopPointSeconds = (double)s.clip.samples / s.clip.frequency;
        }
    }

}
