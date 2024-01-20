using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    AudioSource musicSource;
    private void Awake()
    {
        if (FindObjectsOfType<MusicScript>().Length > 1)
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
        musicSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            musicSource.clip = audioClips[i];
            musicSource.Play();
            yield return new WaitForSeconds(audioClips[i].length);
            if(i == audioClips.Length - 1)
            {
                i = 0;
            }
        }
    }
}
