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
    bool m_isBaseDestroyed = false;

    [SerializeField] GameObject collisionFX;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float baseHealth = 100f;
    [SerializeField] TextMeshProUGUI healthIndicator;
    // [SerializeField] TextMeshProUGUI maxhealthIndicator;

    private void Start()
    {
        m_ImpSource = GetComponent<CinemachineImpulseSource>();
        m_maxHealth = baseHealth;
        UpdateHealth();
    }

    private void Update()
    {
        if(baseHealth == 0 && !m_isBaseDestroyed)
        {
            m_isBaseDestroyed = true;
            BaseDeathSequence();
        }        
    }

    private void BaseDeathSequence()
    {
        m_ImpSource.GenerateImpulse();
        FindObjectOfType<AudioFXManager>().PlayBaseDestructionSound();
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Delay());
            
            baseHealth = Mathf.Clamp(baseHealth - collision.gameObject.GetComponent<EnemyCollisionScript>().GetDamage(), 0, m_maxHealth);            
            m_ImpSource.GenerateImpulse();
            Instantiate(collisionFX, collision.transform.position, Quaternion.identity);
            UpdateHealth();
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

    void UpdateHealth()
    {
        healthIndicator.text = Mathf.Round(baseHealth).ToString() + " %";
    }
}
