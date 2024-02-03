using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
   public void OnResumeButtonClicked()
    {
        Time.timeScale = 1f;
        FindObjectOfType<MenuInstantiate>().ToggleShifter();
        FindObjectOfType<MenuInstantiate>().ReduceAudioVolume(false);
        FindObjectOfType<PostProcessingManipulate>().MenuOff();
        Destroy(gameObject);
    }

    public void OnRestartButtonClicked()
    {
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneindex);
        FindObjectOfType<MenuInstantiate>().ReduceAudioVolume(false);
        Time.timeScale = 1f;
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
        FindObjectOfType<GameMusicScript>().StopGameMusic();
    }
}
