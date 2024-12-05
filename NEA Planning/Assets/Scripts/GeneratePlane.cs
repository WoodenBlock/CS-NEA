using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePlane : MonoBehaviour
{
    [Header("Area Generation")]
    public GameObject plane;
    public GameObject plane1;
    public GameObject parent;
    public int length;

    [Header("Apple Spawning")]
    public GameObject apple;
    public Boolean appleSpawned = false;

    [Header("Parameters")]
    public int scale;
    public (int, int) gridPos;

    [Header("Score")]
    public int score = 0;
    public Boolean scoreChange = false;
    public TextMeshProUGUI scoreDisplay;

    [Header("Game Over")]
    public GameObject gameOverUI;

    [Header("Pausing")]
    public Boolean gamePaused;



    void Update()
    {
        SpawnApple();
        scoreDisplay.text = score.ToString();
    }
    void Start()
    {
        InstantiatePlane();
    }

    void SpawnApple()
    {
        if (!appleSpawned)
        {
            int x = UnityEngine.Random.Range(0, length) * scale * 10;
            int z = UnityEngine.Random.Range(0, length) * scale * 10;
            GameObject newGrid = Instantiate(apple, new Vector3(x, 1f, z), Quaternion.identity, transform);
            
            appleSpawned = true;
        }
    }

    void InstantiatePlane()
    {
        for(int i = 0; i < length * length; i++)
        {
            int x = (i / length) * scale * 10;
            int z = (i % length) * scale * 10;
            if (i % 2 == 0)
            {
                Instantiate(plane, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
            }
            else
            {
                Instantiate(plane1, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
            }
        }
    }

    public void TriggerGameOver()
    {
        gamePaused = true;
        gameOverUI.SetActive(true);
    }

}
