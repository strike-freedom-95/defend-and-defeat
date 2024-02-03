using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    [SerializeField] ParticleSystem powerUpCollectFX;
    [SerializeField] float interval = 10f;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject SpeedEffect;
    [SerializeField] GameObject PointsEffect;

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
        StopCoroutine(Powerup2Activate());
        StopCoroutine(Powerup3Activate());
        StopCoroutine(Powerup4Activate());
        StopCoroutine(Powerup5Activate());
        Instantiate(SpeedEffect, new Vector2(0, 0), Quaternion.identity);
        GetComponent<PlayerMovement>().SetSpeed(20f);
        yield return new WaitForSeconds(6);
        GetComponent<PlayerMovement>().SetSpeed(10f);
    }

    IEnumerator Powerup2Activate()
    {
        StopCoroutine(Powerup1Activate());
        StopCoroutine(Powerup3Activate());
        StopCoroutine(Powerup4Activate());
        StopCoroutine(Powerup5Activate());

        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        int baseCount = bases.Length;
        for (int i = 0; i < baseCount; i++)
        {
            Instantiate(shieldPrefab, bases[i].transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator Powerup3Activate()
    {
        StopCoroutine(Powerup2Activate());
        StopCoroutine(Powerup1Activate());
        StopCoroutine(Powerup4Activate());
        StopCoroutine(Powerup5Activate());

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
    }

    IEnumerator Powerup4Activate()
    {
        StopCoroutine(Powerup2Activate());
        StopCoroutine(Powerup3Activate());
        StopCoroutine(Powerup1Activate());
        StopCoroutine(Powerup5Activate());

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesCount = enemies.Length;
        for (int i = 0; i < enemiesCount; i++)
        {
            if (!enemies[i].IsDestroyed())
            {
                enemies[i].GetComponent<EnemyCollisionScript>().EnemyDestruction();
            }
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator Powerup5Activate()
    {
        StopCoroutine(Powerup2Activate());
        StopCoroutine(Powerup3Activate());
        StopCoroutine(Powerup4Activate());
        StopCoroutine(Powerup1Activate());
        Instantiate(PointsEffect, new Vector2(0, 0), Quaternion.identity);
        GetComponent<PlayerCollision>().SetScoreMultiplier(true);
        yield return new WaitForSeconds(10);
        GetComponent<PlayerCollision>().SetScoreMultiplier(false);
    }
}
