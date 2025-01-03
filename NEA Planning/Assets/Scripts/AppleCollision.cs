using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AppleCollision : MonoBehaviour
{
    public GeneratePlane gameManager;

    private AudioSource sound;

    // Update is called once per frame
    void Start() {
        sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            sound.Play();
            Destroy(other.gameObject);
            gameManager.appleSpawned = false;
            gameManager.scoreChange = true;
            gameManager.score = gameManager.score + 1;
        }
    }

}
