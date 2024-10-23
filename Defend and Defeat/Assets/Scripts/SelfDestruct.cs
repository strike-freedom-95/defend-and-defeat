using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float interval = 2f;
    [SerializeField] bool keepAliveOnSceneChange = false;
    private void Awake()
    {        
        if(!keepAliveOnSceneChange)
        {
            Destroy(gameObject, interval);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(DelayedDestruction());
        }
    }

    IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }
}
