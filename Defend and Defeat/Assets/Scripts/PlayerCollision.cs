using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem collectFX;
    [SerializeField] GameObject contactSFX;
    [SerializeField] GameObject contactFX;
    [SerializeField] AudioClip contact;

    bool isScoreMultiplierActive = false;
    int multiplier = 1;
    int tempScore = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(contact, transform.position, 1f);
            Instantiate(contactSFX, transform.position, Quaternion.identity);
            Instantiate(contactFX, transform.position, Quaternion.identity);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            // int score = collision.GetComponent<CoinScript>().GetScore();
            var FX = Instantiate(collectFX, collision.transform.position, Quaternion.identity);
            ScoreCalculation(collision);
            FX.Play();
            Destroy(collision.gameObject);
        }
    }

    private void ScoreCalculation(Collider2D collision)
    {
        int score = PlayerPrefs.GetInt("Score", 0) + (collision.GetComponent<CoinScript>().GetScore() * multiplier);
        PlayerPrefs.SetInt("Score", score);
        tempScore += (collision.GetComponent<CoinScript>().GetScore() * multiplier);
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

    public int TempScore()
    {
        return tempScore;
    }
}
