using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Player engine audio source")]
    [SerializeField] private AudioSource _engineAudioSource;

    private AudioSource _audioSource;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip) => _audioSource.PlayOneShot(clip, 0.2f);

    public void PlayEngine()
    {
        if(!_engineAudioSource.isPlaying)
            _engineAudioSource.Play();
    }

    public void StopEngine() => _engineAudioSource.Stop();

}
