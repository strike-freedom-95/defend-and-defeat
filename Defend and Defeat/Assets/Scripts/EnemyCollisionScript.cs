using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollisionScript : MonoBehaviour
{
    [SerializeField] float health = 1f;
    [SerializeField] GameObject shockwave;
    // [SerializeField] ParticleSystem explosionFX;
    // [SerializeField] ParticleSystem collisionSparkFX;
    [SerializeField] GameObject[] coinPrefab;
    [SerializeField] float impactForce = 5f;
    [SerializeField] float enemyDamage = 1;
    // [SerializeField] GameObject[] spawnPrefab;

    private void FixedUpdate()
    {
        if(health < 0)
        {
            // SpawnPowerup();
            EnemyDestruction();
        }
    }

    void SpawnCoin()
    {
        /* Instantiate(coinPrefab[Random.Range(0, coinPrefab.Length - 1)], 
            transform.position + new Vector3(Random.Range(-2f, 2f), 
            Random.Range(-2f, 2f), 0), Quaternion.identity);    */

        Instantiate(coinPrefab[Random.Range(0, coinPrefab.Length - 1)], transform.position , Quaternion.identity);
    }

    /* void SpawnPowerup()
    {
        if((int)Random.Range(0, 10) %7 == 0)
        {
            Instantiate(spawnPrefab[Random.Range(0, spawnPrefab.Length - 1)],
            transform.position + new Vector3(Random.Range(-2f, 2f),
            Random.Range(-2f, 2f), 0), Quaternion.identity);
        }        
    }*/ 

    public void EnemyDestruction()
    {
        SpawnCoin();
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        // var FX = Instantiate(explosionFX, transform.position, Quaternion.identity);
        // FX.Play();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * 2f, ForceMode2D.Impulse);
        }

        if(collision.gameObject.tag == "Player")
        {
            // health -= collision.relativeVelocity.magnitude * Random.Range(1, 10);
            health--;
            Instantiate(shockwave, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * impactForce, ForceMode2D.Impulse);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        health--;
        Instantiate(shockwave, transform.position, Quaternion.identity);
    }

    public void InstantDeath()
    {
        EnemyDestruction();
    }

    public float GetDamage()
    {
        return enemyDamage;
    }
}
