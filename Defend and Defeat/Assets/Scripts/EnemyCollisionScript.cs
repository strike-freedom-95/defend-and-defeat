using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollisionScript : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject[] coinPrefab;
    [SerializeField] GameObject[] spawnPrefab;

    private void FixedUpdate()
    {
        if(health < 0)
        {
            SpawnCoin();
            SpawnPowerup();
            EnemyDestruction();
        }
    }

    void SpawnCoin()
    {
        Instantiate(coinPrefab[Random.Range(0, coinPrefab.Length - 1)], 
            transform.position + new Vector3(Random.Range(-2f, 2f), 
            Random.Range(-2f, 2f), 0), Quaternion.identity);        
    }

    void SpawnPowerup()
    {
        if((int)Random.Range(0, 10) %7 == 0)
        {
            Instantiate(spawnPrefab[Random.Range(0, spawnPrefab.Length - 1)],
            transform.position + new Vector3(Random.Range(-2f, 2f),
            Random.Range(-2f, 2f), 0), Quaternion.identity);
        }        
    }

    private void EnemyDestruction()
    {
        var FX = Instantiate(explosionFX, transform.position, Quaternion.identity);
        FX.Play();
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Base")
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * 2f, ForceMode2D.Impulse);
        }

        if(collision.gameObject.tag == "Player")
        {
            health -= collision.relativeVelocity.magnitude * Random.Range(1, 10);
        }
    }
}
