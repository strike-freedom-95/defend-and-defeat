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

    [SerializeField] GameObject ScoreDisplayPrefab;
    [SerializeField] GameObject LevelDisplayPrefab;
    [SerializeField] GameObject StartGameMessage;

    private void Start()
    {
        originalBaseCount = GameObject.FindGameObjectsWithTag("Base").Length;
        Instantiate(ScoreDisplayPrefab, new Vector2(0, 0), Quaternion.identity);
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
        if(bases < originalBaseCount)
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

    void ResetScore()
    {
        int scoreDifference = PlayerPrefs.GetInt("Score") - FindObjectOfType<PlayerCollision>().TempScore();
        PlayerPrefs.SetInt("Score", scoreDifference);
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
            ResetScore();
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;            
        }
        else
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }


}
