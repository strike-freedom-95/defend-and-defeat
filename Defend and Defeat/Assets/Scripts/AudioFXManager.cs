using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    AudioSource m_soundSource;

    private void Start()
    {
        m_soundSource = GetComponent<AudioSource>();
    }

    public void PlayBaseDestructionSound()
    {
        m_soundSource.PlayOneShot(sounds[0]);
    }

    public void PlayEnemySpawnSound()
    {
        m_soundSource.PlayOneShot(sounds[1]);
    }

    public void PlayEnemyCollisionSound()
    {
        m_soundSource.PlayOneShot(sounds[2]);
    }

    public void PlayShieldDestructionSound()
    {
        m_soundSource.PlayOneShot(sounds[3]);
    }
}
