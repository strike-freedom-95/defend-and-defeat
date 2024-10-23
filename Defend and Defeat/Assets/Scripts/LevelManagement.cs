using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    int spawners;
    int bases;
    int enemies;

    // [SerializeField] GameObject ScoreDisplayPrefab;
    [SerializeField] GameObject LevelDisplayPrefab;
    [SerializeField] GameObject StartGameMessage;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Instantiate(LevelDisplayPrefab, new Vector2(0, 0), Quaternion.identity);
        Instantiate(StartGameMessage, new Vector2(0, 0), Quaternion.identity);

        if (PlayerPrefs.GetInt("Progress", 0) < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Progress", SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Update()
    {
        CheckEnemyCount();
        CheckBaseCount();
        CheckSpawnerCount();
        if(spawners == 0 && enemies == 0)
        {
            NextLevel();
        }
        /* if(bases < originalBaseCount)
        {
            ResetGame(false);
        } */
        if (bases == 0)
        {
            ResetGame(false);
        }
    }

    void CheckSpawnerCount()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner").Length;
    }

    void CheckEnemyCount()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void CheckBaseCount()
    {
        bases = GameObject.FindGameObjectsWithTag("Base").Length;
    }

    public void ResetGame(bool isManual)
    {
        ResetSequence();
        if (!isManual)
        {
            FindObjectOfType<PostProcessingManipulate>().GameOverEffects();
            StartCoroutine(SceneChange(3.5f, true));
        }
        else
        {
            StartCoroutine(SceneChange(0, true));
        }

    }

    private static void ResetSequence()
    {        
        if(GameObject.FindGameObjectWithTag("Laser Attack") != null)
        {
            GameObject.FindGameObjectWithTag("Laser Attack").GetComponent<ParticleSystem>().Stop();
        }
        foreach (var powerButtons in GameObject.FindGameObjectsWithTag("Power Buttons"))
        {
            powerButtons.GetComponent<Button>().interactable = false;
        }
    }

    void ResetScore()
    {
        //int scoreDifference = PlayerPrefs.GetInt("Score") - FindObjectOfType<PlayerCollision>().TempScore();
        //PlayerPrefs.SetInt("Score", scoreDifference);
    }

    void NextLevel()
    {
        StartCoroutine(SceneChange(1, false));
    }

    IEnumerator SceneChange(float interval, bool isReset)
    {
        // Destroy(ScoreDisplayPrefab);
        /*GameObject[] levelCheck = GameObject.FindGameObjectsWithTag("Level Check");
        for(int i=0; i < levelCheck.Length; i++)
        {
            Destroy(levelCheck[i]);
        }*/

        int currentSceneIndex;
        yield return new WaitForSeconds(interval);
        if (isReset)
        {
            // ResetScore();
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;            
        }
        else
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }


}
