using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Tilemaps.Tilemap;

public class HeadMovement : MonoBehaviour
{
    [Header("Movement")]

    public float baseSpeed;
    public float speedFactor;
    public float maxSpeed;

    Vector3 moveDirection;

    Rigidbody rb;

    public Transform orientation;

    // Boolean to see if a movement is waiting to be in the correct position to be enabled
    private Boolean qTurnRight = false;
    private Boolean qTurnLeft = false;
    private Boolean allowMove = true;

    // Stores current state
    private Vector3 currentPos;

    [Header("Body Parts")]

    public GeneratePlane gameManager;

    public GameObject snakeBody;

    public LinkedList<Vector3> snakeParts = new LinkedList<Vector3>();
    private LinkedList<GameObject> oldSnakeParts = new LinkedList<GameObject>();



    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3 (0, 3, 0);

    }
    private void FixedUpdate()
    {
        if(!gameManager.gamePaused) { MoveHead(); }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            qTurnRight = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            qTurnLeft = true;
        }
    }

    private void SpawnBodyParts()
    {
        foreach(Vector3 p in snakeParts)
        {
            GameObject snake = Instantiate(snakeBody, p, Quaternion.identity, gameManager.transform);
            oldSnakeParts.AddLast(snake);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject b in oldSnakeParts)
        {
            Destroy(b);
        }
        oldSnakeParts = new LinkedList<GameObject>();
        SpawnBodyParts();

        allowMove = true;
        if (other.gameObject.tag == "TurningPoint")
        {
            currentPos = other.transform.position; 

            if (snakeParts.Contains(currentPos))
            {
                gameManager.TriggerGameOver();
            }

            snakeParts.AddFirst(currentPos);
            if (!gameManager.scoreChange)
            {
                snakeParts.RemoveLast();

            }
            else
            {
                gameManager.scoreChange = false;
            }
            
        }
        else if (other.gameObject.tag == "Wall") {
            print("gameover by wall");
            gameManager.TriggerGameOver();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TurningPoint" && allowMove)
        {
            if(qTurnRight)
            {
                transform.Rotate(0, 90, 0);
                rb.velocity = Vector3.zero;
                transform.position = currentPos + new Vector3(0, 1, 0);
                qTurnRight = false;
                allowMove = false;
            }

            if (qTurnLeft)
            {
                transform.Rotate(0, -90, 0);
                rb.velocity = Vector3.zero;
                transform.position = currentPos + new Vector3(0, 1, 0);
                qTurnLeft = false;
                allowMove = false;
            }
        }
    }



    private void MoveHead()
    {
        float actualSpeed = Math.Min(baseSpeed + gameManager.score * speedFactor, maxSpeed);
        rb.AddForce(orientation.forward * actualSpeed * 10f, ForceMode.Force);
    }
}
