using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource soundEffectPrefab;
    public int initialPoolSize = 5;

    private List<AudioSource> audioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create initial pool of AudioSources
        audioSources = new List<AudioSource>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            AudioSource newSource = Instantiate(soundEffectPrefab, transform);
            audioSources.Add(newSource);
        }
    }

    // Play a sound effect
    public void PlaySoundEffect(AudioClip clip, float volume = 1f)
    {
        // Find an available audio source
        AudioSource source = GetAvailableAudioSource();
        if (source == null)
        {
            Debug.LogWarning("No available audio sources to play sound effect.");
            return;
        }

        // Play the sound effect
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    // Find an available audio source from the pool
    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        // If no available sources found, create a new one
        AudioSource newSource = Instantiate(soundEffectPrefab, transform);
        audioSources.Add(newSource);
        return newSource;
    }
}
