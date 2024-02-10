using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Cursor.visible = GameObject.FindGameObjectsWithTag("Main Menu").Length > 0 || GameObject.FindGameObjectsWithTag("Game Menu").Length > 0;
    }

}
