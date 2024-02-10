using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
    [SerializeField] bool isMenuMusic = false;
    [SerializeField] AudioClip[] menuMusics;
    [SerializeField] AudioClip[] gameMusics;

    private void Awake()
    {
        if (FindObjectsOfType<GameMusicScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        AudioSource musicSource = GetComponent<AudioSource>();
        if (isMenuMusic)
        {
            musicSource.clip = menuMusics[Random.Range(0, menuMusics.Length)];
            musicSource.Play();
        }
        else
        {
            musicSource.clip = gameMusics[Random.Range(0, gameMusics.Length)];
            musicSource.Play();
        }
    }

    public void StopGameMusic()
    {
        Destroy(gameObject);
    }
}
