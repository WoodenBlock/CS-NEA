using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A key press detected");
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * 20);
        }
    }
}
