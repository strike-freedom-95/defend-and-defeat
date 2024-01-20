using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToNextScene : MonoBehaviour
{
    [SerializeField] GameObject fadeIn;
    [SerializeField] AudioClip select;

    bool isSelected = true;

    void Update()
    {
        if (Input.anyKey)
        {        
            Instantiate(fadeIn, new Vector2(0, 0), Quaternion.identity);
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene()
    {
        PlayInterfaceSound();
        yield return new WaitForSeconds(1);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    void PlayInterfaceSound()
    {
        if (isSelected)
        {
            AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 1f);
            isSelected = false;
        }        
    }
}
