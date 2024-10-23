using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControls : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCamera;

    bool isPlayerMoving = false;

    private void Update()
    {
        isPlayerMoving = false;
        if(GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            isPlayerMoving = true;
        }
        followCamera.GetComponent<Animator>().SetBool("isMoving", isPlayerMoving);
    }
}
