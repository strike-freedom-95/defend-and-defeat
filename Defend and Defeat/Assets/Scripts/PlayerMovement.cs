using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rigidBody;
    int m_originalBases;

    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_originalBases = GameObject.FindGameObjectsWithTag("Base").Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");
        if(GameObject.FindGameObjectsWithTag("Base").Length >= m_originalBases)
        {
            m_rigidBody.velocity = new Vector2(hMovement * moveSpeed, vMovement * moveSpeed);
        }       
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
