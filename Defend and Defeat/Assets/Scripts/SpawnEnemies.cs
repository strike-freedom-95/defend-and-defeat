using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemyCount = 10f;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float delay = 2f;

    [Header("Spawn Range for Randomizer")]
    [SerializeField] float m_minRange = -10f;
    [SerializeField] float m_maxRange = 10f;
    [SerializeField] bool isHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnDelay", delay);
    }

    IEnumerator SpawnAtInterval(float interval)
    {        
        for(int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(interval);
            if(isHorizontal)
            {
                Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(m_minRange, m_maxRange), 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab, transform.position + new Vector3(0, Random.Range(m_minRange, m_maxRange), 0), Quaternion.identity);
            }     
        }
        Destroy(gameObject);
    }

    void SpawnDelay()
    {
        StartCoroutine(SpawnAtInterval(spawnInterval));
    }
}
