using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisionScript : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] ParticleSystem explosionFX;

    private void Update()
    {
        if(health < 0)
        {
            EnemyDestruction();
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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if(collision.gameObject.tag == "Player")
        {
            health -= collision.relativeVelocity.magnitude * Random.Range(1, 10);
        }
    }
}
