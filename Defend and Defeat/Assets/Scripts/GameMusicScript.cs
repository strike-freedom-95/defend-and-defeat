using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
    [SerializeField] AudioClip[] music;
    [SerializeField] Canvas musicPlayer;
    [SerializeField]
    string[] musicNames;
    [SerializeField] string artistName;

    AudioSource m_audioSource;
    int m_index;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = music[0];
        m_audioSource.Play();
        SetMusicDetails();
    }

    private void Update()
    {
        if(!m_audioSource.isPlaying)
        {
            Debug.Log("Song Changed");
            ShuffleMusic();
        }
    }

    void ShuffleMusic()
    {
        m_index = Random.Range(0, music.Length);
        AudioClip nextMusic = music[m_index];
        m_audioSource.clip = nextMusic;
        m_audioSource.Play();
        SetMusicDetails();
    }

    void SetMusicDetails()
    {
        var inst = Instantiate(musicPlayer, Vector2.zero, Quaternion.identity);
        inst.GetComponentsInChildren<TextMeshProUGUI>()[0].text = musicNames[m_index];
        inst.GetComponentsInChildren<TextMeshProUGUI>()[1].text = artistName;
    }
}
