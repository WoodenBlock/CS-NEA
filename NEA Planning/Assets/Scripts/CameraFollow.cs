using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position;
        transform.rotation = playerPos.rotation;
    }
}
