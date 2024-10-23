using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rigidBody;
    float m_hMovement, m_vMovement;
    int multiplier = 1;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Joystick joystick;
    [SerializeField] ParticleSystem fire;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // m_hMovement = Input.GetAxisRaw("Horizontal"); ;
        // m_vMovement = Input.GetAxisRaw("Vertical");

        m_hMovement = joystick.Horizontal;
        m_vMovement = joystick.Vertical;
    }
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(GameObject.FindGameObjectsWithTag("Base").Length != 0)
        {
            m_rigidBody.velocity = new Vector2(m_hMovement, m_vMovement) * moveSpeed * multiplier;
        }       
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void PlayerTurboMode()
    {
        multiplier = 2;
        StartCoroutine(DefaultSpeedAfterDelay());
    }

    IEnumerator DefaultSpeedAfterDelay()
    {
        GetComponent<PlayerCollision>().SetInstantKillMode(true);
        fire.Play();
        yield return new WaitForSeconds(5);
        multiplier = 1;
        GetComponent<PlayerCollision>().SetInstantKillMode(false);
        fire.Stop();
    }
}
