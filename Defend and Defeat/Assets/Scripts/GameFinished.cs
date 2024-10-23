using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    GameObject[] m_musicPlayers;
    string m_url = "https://game-mechanoid.itch.io/";

    [SerializeField] AudioClip select;

    public void GoBackToMain()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);        
        SceneManager.LoadScene("Main Menu");
    }

    public void VisitItchIOPage()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 1f);
        // System.Diagnostics.Process.Start(m_url);
        Application.OpenURL(m_url);
    }
}
