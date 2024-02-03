using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    GameObject[] m_musicPlayers;
    public void GoBackToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Start()
    {
        m_musicPlayers = GameObject.FindGameObjectsWithTag("Game Music");
        for(int i = 0;  i < m_musicPlayers.Length; i++)
        {
            Destroy(m_musicPlayers[i]);
        }
        // Instantiate(MusicPrefab);
    }
}
