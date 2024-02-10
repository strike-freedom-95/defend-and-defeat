using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigation : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    public void OnBackButtonClicked()
    {
        PageCloseSequence();
    }

    public void ExternalCloseSignal()
    {
        PageCloseSequence();
    }

    private void Awake()
    {
        if (FindObjectsOfType<PageNavigation>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void PageCloseSequence()
    {
        GetComponent<Animator>().SetTrigger("isClosed");
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
