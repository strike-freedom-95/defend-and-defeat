using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuScript : MonoBehaviour
{
    [SerializeField] GameObject QuitConfirmWindow;
    [SerializeField] Slider volumeSlider;

    GameObject musicPlayer;

    private void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("Music Player");
        if(musicPlayer != null)
        {
            volumeSlider.value = musicPlayer.GetComponent<AudioSource>().volume;
        }
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1f;
        FindObjectOfType<MenuInstantiate>().ToggleShifter();
        // FindObjectOfType<MenuInstantiate>().ReduceAudioVolume(false);
        FindObjectOfType<PostProcessingManipulate>().MenuOff();
        Destroy(gameObject);
    }

    public void OnRestartButtonClicked()
    {
        // int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneindex);
        // FindObjectOfType<MenuInstantiate>().ReduceAudioVolume(false);
        Time.timeScale = 1f;
        FindObjectOfType<LevelManagement>().ResetGame(true);
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
        // Instantiate(QuitConfirmWindow, new Vector2(0, 0), Quaternion.identity);
        Time.timeScale = 1f;
    }

    public void OnButtonClickedSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void OnVolumeSliderChanged()
    {
        if(musicPlayer != null)
        {
            musicPlayer.GetComponent<AudioSource>().volume = volumeSlider.value;
        }
    }
}
