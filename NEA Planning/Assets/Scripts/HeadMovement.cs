using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Tilemaps.Tilemap;

public class HeadMovement : MonoBehaviour
{
    [Header("Movement")]

    public float BaseSpeed;
    public float SpeedFactor;
    public float MaxSpeed;

    private Rigidbody _rb;

    // Boolean to see if a movement is waiting to be in the correct position to be enabled
    private bool _qTurnRight = false;
    private bool _qTurnLeft = false;
    private bool _allowMove = true;

    // Stores current state
    private Vector3 _currentPos;

    [Header("Body Parts")]

    public GeneratePlane GameManager;

    public GameObject SnakeBody;

    public bool QueueGrowth;

    public LinkedList<Vector3> SnakeParts = new LinkedList<Vector3>();
    private LinkedList<GameObject> OldSnakeParts = new LinkedList<GameObject>();



    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetupSnake();
    }

    private void SetupSnake () {
        // resets snake heads position + rotation
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3 (0, 3, 0);
        // resets stored snake parts
        SnakeParts = new LinkedList<Vector3>();
        // removes all spawned snake parts
        foreach (GameObject part in OldSnakeParts) {
            Destroy(part);
        }
        OldSnakeParts = new LinkedList<GameObject>();
    }
    private void FixedUpdate()
    {
        if(!GameManager.GamePaused) { MoveHead(); }
        else
        {
            // freezes velocity to 'pause' game
            _rb.velocity = Vector3.zero;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            _qTurnRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _qTurnLeft = true;
        }
    }

    private void SpawnBodyParts()
    {
        foreach(Vector3 p in SnakeParts)
        {
            // spawns a snake part at each coordinate in array, and stores this part in linked list
            GameObject snakePart = Instantiate(SnakeBody, p, Quaternion.identity, GameManager.transform);
            OldSnakeParts.AddLast(snakePart);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // removes all snake parts from last collision enter
        foreach(GameObject b in OldSnakeParts)
        {
            Destroy(b);
        }
        // resets the linked list to remove old objects
        OldSnakeParts = new LinkedList<GameObject>();

        SpawnBodyParts();

        _allowMove = true;

        if (other.gameObject.tag == "TurningPoint")
        {
            _currentPos = other.transform.position; 

            if (SnakeParts.Contains(_currentPos))
            {
                GameManager.TriggerGameOver();
             SetupSnake();
            }

            SnakeParts.AddFirst(_currentPos);

            if (QueueGrowth)
            {
                QueueGrowth = false;
            }
            else
            {
                SnakeParts.RemoveLast();
            }
            
        }
        // trigger game over by wall
        else if (other.gameObject.tag == "Wall") {
            GameManager.TriggerGameOver();
            SetupSnake();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TurningPoint" && _allowMove)
        {
            if(_qTurnRight)
            {
                transform.Rotate(0, 90, 0);
                _rb.velocity = Vector3.zero;
                transform.position = _currentPos + new Vector3(0, 1, 0);
                _qTurnRight = false;
                _allowMove = false;
            }

            else if (_qTurnLeft)
            {
                transform.Rotate(0, -90, 0);
                _rb.velocity = Vector3.zero;
                transform.position = _currentPos + new Vector3(0, 1, 0);
                _qTurnLeft = false;
                _allowMove = false;
            }
        }
    }

    private void MoveHead()
    {
        float actualSpeed = Math.Min(BaseSpeed + GameManager.Score * SpeedFactor, MaxSpeed);
        _rb.AddForce(transform.forward * actualSpeed * 10f, ForceMode.Force);
    }
}
