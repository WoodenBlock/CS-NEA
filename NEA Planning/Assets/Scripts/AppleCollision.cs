using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollision : MonoBehaviour
{
    public GeneratePlane gameManager;

    private HeadMovement headMovement;
    private AudioSource sound;

    // Creats link to the object that this script is attached 
    // to's AudioSource component if it has one
    void Start() {
        sound = GetComponent<AudioSource>();
        headMovement = GetComponent<HeadMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            sound.Play();
            Destroy(other.gameObject);
            gameManager.AppleSpawned = false;
            headMovement.QueueGrowth = true;
            // Increases score and tells gameManager to update elements
            gameManager.IsScoreChange = true;
            gameManager.Score = gameManager.Score + 1;
        }
    }

}
