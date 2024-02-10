using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    GameObject[] m_musicPlayers;
    
    [SerializeField] AudioClip select;

    public void GoBackToMain()
    {
        StartCoroutine(Delay());
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

    IEnumerator Delay()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        yield return new WaitForSeconds(0.4f);        
        SceneManager.LoadScene("Main Menu");
    }
}
