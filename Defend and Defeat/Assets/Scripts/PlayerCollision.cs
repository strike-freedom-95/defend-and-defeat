using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem collectFX;
    [SerializeField] GameObject contactSFX;
    [SerializeField] GameObject contactFX;

    bool isScoreMultiplierActive = false;
    int multiplier = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(contactSFX, transform.position, Quaternion.identity);
            Instantiate(contactFX, transform.position, Quaternion.identity);
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
            GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreKeeper>().UpdateScore(score * multiplier);
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (isScoreMultiplierActive)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
    }

    public void SetScoreMultiplier(bool status)
    {
        isScoreMultiplierActive = status;
    }
}
