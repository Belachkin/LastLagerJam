using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioMixerGroup audioMixer;

    public Sound[] Sounds;

    private void Awake()
    {
        foreach (var s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;   

        }
    }

    void Start()
    {      
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(Sounds, s => s.name == name);
        sound.source.Play();
    
    }

}
