using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject[] collectFX;
    [SerializeField] GameObject energyWave;
    [SerializeField] int maxPower = 10000;

    bool isInstantDeathActive = false;
    int power;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (isInstantDeathActive)
            {
                collision.gameObject.GetComponent<EnemyCollisionScript>().InstantDeath();
            }
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            PowerCalculation(collision.gameObject.GetComponent<CoinScript>().GetPower());
            if (power < maxPower)
            {
                if (collision.gameObject.GetComponent<CoinScript>().GetPower() == 100)
                {
                    Instantiate(collectFX[0], transform.position, Quaternion.identity);
                }
                else if (collision.gameObject.GetComponent<CoinScript>().GetPower() == 250)
                {
                    Instantiate(collectFX[1], transform.position, Quaternion.identity);
                }
                else if (collision.gameObject.GetComponent<CoinScript>().GetPower() == 500)
                {
                    Instantiate(collectFX[2], transform.position, Quaternion.identity);
                }
                Instantiate(energyWave, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }            
        }
    }

    private void PowerCalculation(int collected)
    {
        power = Mathf.Clamp(power + (collected * 2), 0, maxPower);
    }

    public int GetCurrentCollectedPower()
    {
        return power;
    }

    public void ResetCollectedPower()
    {
        power = 0;
    }

    public int GetMaxPower()
    {
        return maxPower;
    }

    public void SetInstantKillMode(bool status)
    {
        isInstantDeathActive = status;
    }
}
