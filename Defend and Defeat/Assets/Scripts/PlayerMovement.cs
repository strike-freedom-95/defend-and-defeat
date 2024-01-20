using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rigidBody; 

    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
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
        if(GameObject.FindGameObjectsWithTag("Base").Length > 0)
        {
            m_rigidBody.velocity = new Vector2(hMovement * moveSpeed, vMovement * moveSpeed);
        }       
    }
}
