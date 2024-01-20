using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float interval = 2f;
    private void Awake()
    {
        Destroy(gameObject, interval);
    }
}
