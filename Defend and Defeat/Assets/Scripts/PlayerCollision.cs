using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem collectFX;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            int score = collision.GetComponent<CoinScript>().GetScore();
            var FX = Instantiate(collectFX, collision.transform.position, Quaternion.identity);
            FX.Play();
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreKeeper>().UpdateScore(score);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Powerup")
        {
            var FX = Instantiate(collectFX, collision.transform.position, Quaternion.identity);
            FX.Play();
            Destroy(collision.gameObject);
        }
    }
}
