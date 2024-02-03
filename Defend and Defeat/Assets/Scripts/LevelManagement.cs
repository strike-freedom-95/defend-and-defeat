using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    int spawners;
    int bases;
    int enemies;
    int originalBaseCount;

    private void Start()
    {
        originalBaseCount = GameObject.FindGameObjectsWithTag("Base").Length;
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
        if(bases < originalBaseCount)
        {
            ResetGame();
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

    void ResetGame()
    {
        FindObjectOfType<PostProcessingManipulate>().GameOverEffects();
        StartCoroutine(SetDelay(3.5f, true));
    }

    void NextLevel()
    {
        StartCoroutine(SetDelay(1, false));
    }

    IEnumerator SetDelay(float interval, bool isReset)
    {
        int currentSceneIndex;
        yield return new WaitForSeconds(interval);
        if (isReset)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;            
        }
        else
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
}
