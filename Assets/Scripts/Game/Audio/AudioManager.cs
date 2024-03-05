using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    void Awake()
    {
        SetupSingleton();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlaySelect()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "MenuSelect");
        s.source.Play();
    }

    private void SetupSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }
}
