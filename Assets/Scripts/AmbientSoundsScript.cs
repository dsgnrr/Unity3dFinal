using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundsScript : MonoBehaviour
{
    private AudioSource dayForest;
    private AudioSource nightForest;
    void Start()
    {
        AudioSource[] sounds = GetComponents<AudioSource>();
        dayForest = sounds[0];
        nightForest = sounds[1];
        GameState.Subscribe(OnGameStateChange);
        EstablishSounds();
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChange);
    }
    private void OnGameStateChange(string propName)
    {
        if (propName == nameof(GameState.IsNight))
        {
            EstablishSounds();
        }
    }

    private void EstablishSounds()
    {
        if (GameState.IsNight)
        {
            dayForest.Stop();
            if (!nightForest.isPlaying)
            {
                nightForest.Play();
            }
        }
        else
        {
            nightForest.Stop();
            if (!dayForest.isPlaying)
            {
                dayForest.Play();
            }
        }
    }
}
