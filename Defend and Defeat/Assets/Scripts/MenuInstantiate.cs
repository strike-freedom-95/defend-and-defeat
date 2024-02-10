using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiate : MonoBehaviour
{
    [SerializeField] GameObject menuView;
    [SerializeField] float delay = 0.5f;

    bool m_isMenuActivated = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(!m_isMenuActivated)
            {
                ReduceAudioVolume(true);
                Instantiate(menuView, new Vector2(0, 0), Quaternion.identity);
                StartCoroutine(PauseDelay());
                m_isMenuActivated = true;
                FindObjectOfType<PostProcessingManipulate>().MenuOn();
            }
            else
            {
                if(menuView != null)
                {                    
                    for(int i = 0; i < GameObject.FindGameObjectsWithTag("Game Menu").Length; i++)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("Game Menu")[i]);
                    }
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Main Menu").Length; i++)
                    {
                        Destroy(GameObject.FindGameObjectsWithTag("Main Menu")[i]);
                    }
                    Time.timeScale = 1f;
                    ReduceAudioVolume(false);
                    m_isMenuActivated = false;
                    FindObjectOfType<PostProcessingManipulate>().MenuOff();
                }                
            }
        }
    }

    public void ReduceAudioVolume(bool status)
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("Game Music");
        for(int i = 0; i < musicPlayers.Length; i++)
        {            
            if (status)
            {
                musicPlayers[i].GetComponent<AudioSource>().Pause();
            }
            else
            {
                musicPlayers[i].GetComponent<AudioSource>().Play();
            }
        }
    }

    IEnumerator PauseDelay()
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
    }

    public void ToggleShifter()
    {
        m_isMenuActivated = false;
    }
}
