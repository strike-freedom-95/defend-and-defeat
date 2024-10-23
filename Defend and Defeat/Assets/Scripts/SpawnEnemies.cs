using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemyCount = 10f;
    [SerializeField] float minSpawnTime, maxSpawnTime;
    [SerializeField] float delay = 2f;

    [Header("Spawn Range for Randomizer")]
    [SerializeField] float m_minRange, m_maxRange;
    [SerializeField] bool isHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAtInterval());
    }

    IEnumerator SpawnAtInterval()
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            if(isHorizontal)
            {
                Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(m_minRange, m_maxRange), 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(enemyPrefab, transform.position + new Vector3(0, Random.Range(m_minRange, m_maxRange), 0), Quaternion.identity);
            }     
        }
        SpawnerDeath();
    }

    void SpawnerDeath()
    {
        GetComponent<Animator>().SetTrigger("isDead");
        Destroy(gameObject, 1f);
    }
}
