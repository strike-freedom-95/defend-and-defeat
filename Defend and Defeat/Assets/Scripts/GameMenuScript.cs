using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
    [SerializeField] GameObject QuitConfirmWindow;
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
        // int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneindex);
        FindObjectOfType<MenuInstantiate>().ReduceAudioVolume(false);
        Time.timeScale = 1f;
        FindObjectOfType<LevelManagement>().ResetGame(true);
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
        // Instantiate(QuitConfirmWindow, new Vector2(0, 0), Quaternion.identity);
        Time.timeScale = 1f;
        FindObjectOfType<GameMusicScript>().StopGameMusic();
    }
}
