using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource _audioSource;

    public void Awake()
    {
        if (Instance != null) Debug.LogError("Instance already Found!?!");
        Instance ??= this;
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    [field: FormerlySerializedAs("SoundEnabled")]
    public bool SoundEnabled { get; set; }

    private bool _musicEnabled;

    public bool MusicEnabled
    {
        get => _musicEnabled;
        set
        {
            _musicEnabled = value;
            _audioSource.mute = !value;
        }
    }
}