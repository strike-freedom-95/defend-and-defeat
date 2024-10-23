using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject enemyProp;

    bool isLocked = false;

    private void Start()
    {
        isLocked = true;
        StartCoroutine(DelayedUnlock());
    }

    private void OnMouseDown()
    {
        if (!isLocked)
        {
            isLocked = true;
            Instantiate(enemyProp, transform.position, Quaternion.identity);
            StartCoroutine(DelayedUnlock());
        }        
    }

    IEnumerator DelayedUnlock()
    {
        yield return new WaitForSeconds(1);
        isLocked = false;
    }
}
