using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image storyPanel;
    [SerializeField] Image creditsPanel;
    [SerializeField] GameObject fadeIn;
    [SerializeField] AudioClip select;
    [SerializeField] TextMeshProUGUI highScoreDisplay;
    [SerializeField] Button continueButton;
    [SerializeField] GameObject InstructionsPage;
    [SerializeField] GameObject CreditsPage;

    GameObject[] gameMusic;

    private void Start()
    {
        gameMusic = GameObject.FindGameObjectsWithTag("Game Music");
        highScoreDisplay.text = "Highscore : " + PlayerPrefs.GetInt("Highscore", 0).ToString();
        int progress = PlayerPrefs.GetInt("Progress", 0);
        if(progress > 2)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PageNavigation temp = FindObjectOfType<PageNavigation>();
            if(temp != null)
            {
                temp.ExternalCloseSignal();
            }
        }

        if (Input.GetMouseButton(0))
        {
            PageNavigation temp = FindObjectOfType<PageNavigation>();
            if (temp != null)
            {
                temp.ExternalCloseSignal();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            GameRefresh();
        }
    }

    private void GameRefresh()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(Refresh());
    }

    public void OnStartButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        Transition();
        Instantiate(fadeIn, new Vector2(0, 0), Quaternion.identity);
        for (int i = 0; i < gameMusic.Length; i++)
        {
            Destroy(gameMusic[i]);
        }
    }

    private void Transition()
    {
        StartCoroutine(SceneChange());
    }

    public void OnContinueButtonClicked()
    {
        PlayerPrefs.SetInt("Score", 0);
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        StartCoroutine(ContinueGame());
        Instantiate(fadeIn, new Vector2(0, 0), Quaternion.identity);
        for (int i = 0; i < gameMusic.Length; i++)
        {
            Destroy(gameMusic[i]);
        }
    }

    public void OnInstructionsButtonClicked()
    {
        // AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        Instantiate(InstructionsPage, new Vector2(0, 0), Quaternion.identity);
        // storyPanel.gameObject.SetActive(true);
        // creditsPanel.gameObject.SetActive(false);
    }

    /*public void OnStoryBackButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        CreditsPage.GetComponent<Animator>().SetBool("isClosed", true);
        InstructionsPage.GetComponent<Animator>().SetBool("isClosed", true);
    }*/

    public void OnCreditsButtonClicked()
    {
        
        // AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        Instantiate(CreditsPage, new Vector2(0, 0), Quaternion.identity);
        // creditsPanel.gameObject.SetActive(true);
        // fstoryPanel.gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        Application.Quit();
    }

    void MovePanel(float initialPos, float finalPos)
    {

    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level 1");
    }

    IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Progress", 0));
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
    }
}
