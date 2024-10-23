using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropEnemyScript : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;

    [SerializeField] float timeToLive = 10f;

    private void Awake()
    {
        Destroy(gameObject, timeToLive);
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
    }

    private void FixedUpdate()
    {
        m_Rigidbody.AddRelativeForce(Vector2.up * 2);
    }
}
