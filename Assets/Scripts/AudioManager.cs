using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Script managing every audio in the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Main Mixer")]
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    [Header("GameObject with all AudioSources")]
    [SerializeField] private GameObject audioSources;
    
    [Header("All the clips")]
    [SerializeField] private Sounds[] sounds;

    //Singleton initialization
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        if (audioSources == null)
        {
            audioSources = gameObject;
        }
        InitializeAllClips();
    }

    /// <summary>
    /// Create an audio source for each clip and set it with the right parameter
    /// </summary>
    void InitializeAllClips()
    {
        foreach (Sounds s in sounds)
        {
            s.source = audioSources.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    /// <summary>
    /// Play a clip
    /// </summary>
    /// <param name="name">The name of the clip</param>
    public void PlayClip(string name)
    {
        if (name == "")
        {
            return;
        }
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The clip " + name + " doesn't exist !");
            return;
        }
        s.source.Play();
    }
}

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}
