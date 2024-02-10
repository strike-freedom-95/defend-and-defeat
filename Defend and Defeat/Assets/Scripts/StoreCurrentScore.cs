using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreCurrentScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {        
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
    }

    private void Update()
    {
        scoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
        ScoreCompare();
    }

    void ScoreCompare()
    {
        if(PlayerPrefs.GetInt("Score", 0) > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Score", 0));
        }
    }
}
