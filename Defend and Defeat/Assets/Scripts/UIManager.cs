using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image storyPanel;
    [SerializeField] Image creditsPanel;
    [SerializeField] GameObject fadeIn;
    [SerializeField] AudioClip select;

    GameObject[] gameMusic;

    private void Awake()
    {
        storyPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        gameMusic = GameObject.FindGameObjectsWithTag("Game Music");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            storyPanel.gameObject.SetActive(false);
            creditsPanel.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            storyPanel.gameObject.SetActive(false);
            creditsPanel.gameObject.SetActive(false);
        }
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
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
    }

    public void OnInstructionsButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        storyPanel.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
    }

    public void OnStoryBackButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        storyPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
    }

    public void OnCreditsButtonClicked()
    {
        AudioSource.PlayClipAtPoint(select, Camera.main.transform.position, 0.4f);
        creditsPanel.gameObject.SetActive(true);
        storyPanel.gameObject.SetActive(false);
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
}
