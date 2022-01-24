using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public SoundFile[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.instance.OnBackgroundVolumeChanged += (value) => BackgroundVolumeChanged(value);
        GameEvents.instance.OnVFXVolumeChanged += (value) => VFXVolumeChanged(value);

        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach( SoundFile s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        SoundFile s =  Array.Find(sounds, sound => sound.name == name);
        if (s== null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    void VFXVolumeChanged(float volume)
    {
        foreach(SoundFile sf in sounds)
        {
            if(sf.name != "Theme")
            {
                sf.source.volume = volume;
            }
        }
    }

    void BackgroundVolumeChanged(float volume)
    {
        SoundFile theme = FindSound("Theme");

        theme.source.volume = volume;
    }

    public SoundFile FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
