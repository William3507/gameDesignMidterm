using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset = new Vector3(0, 2, -10);
    public float delay = 0.25f;
    Vector3 currentVelocity;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position,player.position + camOffset,ref currentVelocity,delay);
    }
}
