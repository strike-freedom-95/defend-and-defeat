using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseCollisionHandler : MonoBehaviour
{
    CinemachineImpulseSource m_ImpSource;
    float m_maxHealth = 0;
    bool m_warningReset = false;

    [SerializeField] ParticleSystem collisionFX;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] float baseHealth = 1000f;
    [SerializeField] TextMeshProUGUI healthIndicator;
    [SerializeField] TextMeshProUGUI maxhealthIndicator;
    [SerializeField] AudioClip hitWarningSFX;

    private void Start()
    {
        m_ImpSource = GetComponent<CinemachineImpulseSource>();
        m_maxHealth = baseHealth;
        healthIndicator.text = m_maxHealth.ToString();
        maxhealthIndicator.text = baseHealth.ToString();
    }

    private void Update()
    {
        if(baseHealth == 0)
        {
            BaseDeathSequence();
        }
        healthIndicator.text = Mathf.Round(baseHealth).ToString();
    }

    private void BaseDeathSequence()
    {
        m_ImpSource.GenerateImpulse();
        FindObjectOfType<AudioFXManager>().PlayBaseDestructionSound();
        var FX = Instantiate(explosionFX, transform.position, Quaternion.identity);
        FX.Play();
        DestroyPowerupUI();
        Destroy(gameObject);
    }

    private static void DestroyPowerupUI()
    {
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUI");
        for (int i = 0; i < powerUps.Length; i++)
        {
            Destroy(powerUps[i]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Delay());
            
            baseHealth = Mathf.Clamp(baseHealth - collision.relativeVelocity.magnitude, 0, m_maxHealth);            
            m_ImpSource.GenerateImpulse();
            var FX = Instantiate(collisionFX, collision.transform.position, Quaternion.identity);
            FX.Play();
        }
    }

    IEnumerator Delay()
    {
        if (!m_warningReset)
        {
            // AudioSource.PlayClipAtPoint(hitWarningSFX, Camera.main.transform.position, 1f);
            m_warningReset = true;
        }
        yield return new WaitForSeconds(9f);
        m_warningReset = false;
    }
}
