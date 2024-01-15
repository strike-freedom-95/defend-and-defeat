using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image storyPanel;
    [SerializeField] Image creditsPanel;
    private void Awake()
    {
        storyPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
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
        SceneManager.LoadScene("Level 1");
    }

    public void OnContinueButtonClicked()
    {

    }

    public void OnInstructionsButtonClicked()
    {
        storyPanel.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
    }

    public void OnStoryBackButtonClicked()
    {
        storyPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
    }

    public void OnCreditsButtonClicked()
    {
        creditsPanel.gameObject.SetActive(true);
        storyPanel.gameObject.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {

    }

    void MovePanel(float initialPos, float finalPos)
    {

    }
}
