using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] float shieldHealth = 100f;
    [SerializeField] float pushbackForce = 5f;

    SpriteRenderer m_spriteRenderer;
    AudioSource m_audioSource;
    bool isShieldDestroyed = false;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_audioSource = GetComponent<AudioSource>();
        m_spriteRenderer.color = Color.cyan;
    }

    private void Update()
    {
        if(shieldHealth < 0 && !isShieldDestroyed)
        {
            isShieldDestroyed = true;
            ShieldDestructionSequence();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            m_audioSource.Play();
            shieldHealth--;
            collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * pushbackForce, ForceMode2D.Impulse);
            m_spriteRenderer.color = Color.red;
            StartCoroutine(DefaultColorAfterDelay());
        }
    }

    void ShieldDestructionSequence()
    {
        FindObjectOfType<AudioFXManager>().PlayShieldDestructionSound();
        Destroy(gameObject);
    }

    IEnumerator DefaultColorAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        m_spriteRenderer.color = Color.cyan;
    }
}
