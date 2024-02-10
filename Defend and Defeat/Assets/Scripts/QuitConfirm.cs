using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitConfirm : MonoBehaviour
{
    bool m_isGameQuit = false;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            m_isGameQuit = true;
        }
    }
    public void OnYesButtonPressed()
    {
        if (m_isGameQuit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void OnNoButtonPressed()
    {
        Time.timeScale = 1f;
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
