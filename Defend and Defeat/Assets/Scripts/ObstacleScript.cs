using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] bool isMovingHorizontally = true;

    int direction = 1;
    float moveSpeed = 1f;

    private void Start()
    {
        moveSpeed = Random.Range(1, 3);
        direction = Random.Range(0, 2) * 2 - 1;
    }

    private void Update()
    {
        if (isMovingHorizontally)
        {
            transform.Translate(Vector2.right * direction * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector2.up * direction * Time.deltaTime * moveSpeed);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Wall")
        {
            direction = direction * -1;
        }       
    }
}
