using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AppleCollision : MonoBehaviour
{
    public GeneratePlane gameManager;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            Destroy(other.gameObject);
            gameManager.appleSpawned = false;
            gameManager.scoreChange = true;
            gameManager.score = gameManager.score + 1;
        }
    }

}
