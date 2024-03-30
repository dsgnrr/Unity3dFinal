using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundsScript : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource audioSource;

    private int _selectedIndex = -1;
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_selectedIndex != value)
            {
                _selectedIndex = value;
                UpdateClip();
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void UpdateClip()
    {
        if (_selectedIndex > -1 && _selectedIndex < audioClips.Count)
        {
            if (audioSource.clip != audioClips[_selectedIndex])
            {
                audioSource.clip = audioClips[_selectedIndex];
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }
}

