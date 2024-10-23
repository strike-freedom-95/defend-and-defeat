using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    [SerializeField] Slider powerSlider;
    [SerializeField] Button shieldButton, laserButton, turboButton;
    [SerializeField] GameObject shield;
    [SerializeField] ParticleSystem lasers;
    [SerializeField] AudioClip shieldSound, laserSound, turboSound;
    [SerializeField] Canvas tutorialCanvas;

    GameObject m_player;
    AudioSource m_audioSource;
    bool isTutorialDisplayed = false;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_audioSource = GetComponent<AudioSource>();
        if(m_player != null)
        {
            powerSlider.maxValue = m_player.GetComponent<PlayerCollision>().GetMaxPower();
        }
    }

    private void Update()
    {
        if(m_player != null)
        {
            powerSlider.value = m_player.GetComponent<PlayerCollision>().GetCurrentCollectedPower();
            if (m_player.GetComponent<PlayerCollision>().GetCurrentCollectedPower() == powerSlider.maxValue)
            {
                ButtonActivationControls(true);
                if (SceneManager.GetActiveScene().buildIndex == 3 && !isTutorialDisplayed)
                {
                    isTutorialDisplayed = true;
                    Instantiate(tutorialCanvas, Vector2.zero, Quaternion.identity);
                }
            }
        }
    }

    void ButtonActivationControls(bool status)
    {
        shieldButton.interactable = status;
        laserButton.interactable = status;
        turboButton.interactable = status;
    }

    public void OnShieldButtonClicked()
    {
        DestroyTutorialPage();
        m_audioSource.PlayOneShot(shieldSound);
        InstantiateShields();
        ButtonActivationControls(false);
        ResetPowerStats();

    }

    public void OnAttackButtonClicked()
    {
        DestroyTutorialPage();
        m_audioSource.PlayOneShot(laserSound);
        InstantiateLasers();
        ButtonActivationControls(false);
        ResetPowerStats();
    }

    public void OnTurboButtonClicked()
    {
        DestroyTutorialPage();
        m_audioSource.PlayOneShot(turboSound);
        ActivateTurboMode();
        ButtonActivationControls(false);
        ResetPowerStats();
    }

    void ResetPowerStats()
    {
        if(m_player != null)
        {
            m_player.GetComponent<PlayerCollision>().ResetCollectedPower();
        }
    }

    void InstantiateShields()
    {
        ClearOldShields();
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        foreach(var instance in bases)
        {
            Instantiate(shield, instance.transform.position, Quaternion.identity);
        }
    }

    void InstantiateLasers()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        foreach (var instance in bases)
        {
            Instantiate(lasers, instance.transform.position, Quaternion.identity);
        }
    }

    void ActivateTurboMode()
    {
        if(m_player != null)
        {
            m_player.GetComponent<PlayerMovement>().PlayerTurboMode();
        }
    }

    void DestroyTutorialPage()
    {
        if(GameObject.FindGameObjectWithTag("Tutorial Page") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tutorial Page"));
        }
    }

    void ClearOldShields()
    {
        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
        foreach (var shield in shields)
        {
            Destroy(shield);
        }
    }

    public void OnPauseButtonPressed()
    {
        FindObjectOfType<MenuInstantiate>().ShowPauseMenu();
    }
}
