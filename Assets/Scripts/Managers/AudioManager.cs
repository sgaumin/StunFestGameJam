using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] plugSounds;
    [SerializeField] private AudioClip unplugSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayUnplugSound()
    {
        _audioSource.clip = unplugSound;
        _audioSource.Play();
    }


    public void PlayPlugSound()
    {
        _audioSource.clip = plugSounds[Random.Range(0, plugSounds.Length)];
        _audioSource.Play();
    }


}
