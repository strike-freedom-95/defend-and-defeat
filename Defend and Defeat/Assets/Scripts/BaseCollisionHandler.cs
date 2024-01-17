using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    CinemachineImpulseSource m_ImpSource;
    float m_maxHealth = 0;

    [SerializeField] ParticleSystem collisionFX;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] float baseHealth = 1000f;

    private void Start()
    {
        m_ImpSource = GetComponent<CinemachineImpulseSource>();
        m_maxHealth = baseHealth;
    }

    private void Update()
    {
        if(baseHealth == 0)
        {
            m_ImpSource.GenerateImpulse();
            var FX = Instantiate(explosionFX, transform.position, Quaternion.identity);
            FX.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Enemy")
        {
            baseHealth = Mathf.Clamp(baseHealth - 1f, 0, m_maxHealth);            
            m_ImpSource.GenerateImpulse();
            var FX = Instantiate(collisionFX, collision.transform.position, Quaternion.identity);
            FX.Play();
        }
    }
}
