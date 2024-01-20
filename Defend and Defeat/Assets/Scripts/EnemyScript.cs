using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject[] m_bases;
    Rigidbody2D m_Rigidbody;
    GameObject m_target;

    [SerializeField] float enemyMoveSpeed = 10f;

    void Start()
    {
        m_bases = GameObject.FindGameObjectsWithTag("Base");
        if(m_bases.Length > 0)
        {
            m_target = m_bases[(int)Random.Range(0, m_bases.Length)];
        }        
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // CUSTOM PRIVATE METHODS

    private void FixedUpdate()
    {
        EnemyMovement();        
    }

    private void EnemyMovement()
    {
        float angle;
        if (m_target != null)
        {
            Vector3 relative = transform.InverseTransformPoint(m_target.transform.position);
            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);
            m_Rigidbody.AddRelativeForce(Vector2.up * enemyMoveSpeed);
        }        
    }
}
