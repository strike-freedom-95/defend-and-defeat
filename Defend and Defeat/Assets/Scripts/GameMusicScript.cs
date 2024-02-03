using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<GameMusicScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StopGameMusic()
    {
        Destroy(gameObject);
    }
}
