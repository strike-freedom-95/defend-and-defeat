using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] ParticleSystem powerUpCollectFX;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject SpeedEffect;
    [SerializeField] GameObject PointsEffect;
    [SerializeField] AudioClip collect;

    bool m_PU1_active = false;
    bool m_PU2_active = false;
    bool m_PU3_active = false;
    bool m_PU4_active = false;
    bool m_PU5_active = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PU1")
        {
            PowerupCollected(collision.gameObject);
            SpeedBoost();
        }
        else if (collision.gameObject.tag == "PU2")
        {
            PowerupCollected(collision.gameObject);
            BaseShield();
        }
        else if (collision.gameObject.tag == "PU3")
        {
            PowerupCollected(collision.gameObject);
            AllStop();
        }
        else if (collision.gameObject.tag == "PU4")
        {
            PowerupCollected(collision.gameObject);
            AllDead();
        }
        else if (collision.gameObject.tag == "PU5")
        {
            PowerupCollected(collision.gameObject);
            ExtraPoints();
        }
    }

    void PowerupCollected(GameObject collected)
    {
        // AudioSource.PlayClipAtPoint(collect, Camera.main.transform.position, 1f);
        var FX = Instantiate(powerUpCollectFX, collected.transform.position, Quaternion.identity);
        FX.Play();
        Destroy(collected);
    }

    void SpeedBoost()
    {
        StartCoroutine(Powerup1Activate());
    }

    void BaseShield()
    {
        StartCoroutine(Powerup2Activate());
    }

    void AllStop()
    {
        StartCoroutine(Powerup3Activate());
    }

    void AllDead()
    {
        StartCoroutine(Powerup4Activate());
    }

    void ExtraPoints()
    {
        StartCoroutine(Powerup5Activate());
    }

    IEnumerator Powerup1Activate()
    {
        if (m_PU1_active)
        {
            yield break;
        }
        m_PU1_active = true;
        
        Instantiate(SpeedEffect, new Vector2(0, 0), Quaternion.identity);
        GetComponent<PlayerMovement>().SetSpeed(20f);

        yield return new WaitForSeconds(6);
        GetComponent<PlayerMovement>().SetSpeed(10f);
        m_PU1_active = false;
    }

    IEnumerator Powerup2Activate()
    {
        if (m_PU2_active)
        {
            yield break;
        }
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        int baseCount = bases.Length;
        m_PU2_active = true;
        
        for (int i = 0; i < baseCount; i++)
        {
            var shield = Instantiate(shieldPrefab, bases[i].transform.position, Quaternion.identity);
            if(i > 1)
            {
                shield.GetComponent<AudioSource>().enabled = false;
            }
        }

        yield return new WaitForSeconds(1);
        m_PU2_active = false;
    }

    IEnumerator Powerup3Activate()
    {
        if (m_PU3_active)
        {
            yield break;
        }

        m_PU3_active = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesCount = enemies.Length;
        for(int i = 0; i < enemiesCount; i++)
        {
            if (!enemies[i].IsDestroyed())
            {
                enemies[i].GetComponent<EnemyScript>().enabled = false;
                enemies[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }            
        }

        yield return new WaitForSeconds(5);
        for (int i = 0; i < enemiesCount; i++)
        {
            if (!enemies[i].IsDestroyed())
            {
                enemies[i].GetComponent<EnemyScript>().enabled = true;
            }            
        }
        m_PU3_active = false;
    }

    IEnumerator Powerup4Activate()
    {
        if (m_PU4_active)
        {
            yield break;
        }

        m_PU4_active = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        StartCoroutine(DelayedExplosion(enemies));

        yield return new WaitForSeconds(1);
        m_PU4_active = false;
    }

    IEnumerator Powerup5Activate()
    {
        if (m_PU5_active)
        {
            yield break;
        }

        m_PU5_active = true;
        StopCoroutine(Powerup2Activate());
        StopCoroutine(Powerup3Activate());
        StopCoroutine(Powerup4Activate());
        StopCoroutine(Powerup1Activate());
        Instantiate(PointsEffect, new Vector2(0, 0), Quaternion.identity);
        m_PU5_active = false;
    }

    IEnumerator DelayedExplosion(GameObject[] enemies)
    {
        for(int i= 0; i < enemies.Length; i++)
        {
            if (!enemies[i].IsDestroyed())
            {
                enemies[i].GetComponent<EnemyCollisionScript>().EnemyDestruction();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
