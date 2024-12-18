using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneratePlane : MonoBehaviour
{
    [Header("Area Generation")]
    public GameObject plane;
    public GameObject plane1;
    public GameObject parent;
    public GameObject wall;
    public int length;

    [Header("Apple Spawning")]
    public GameObject apple;
    public Boolean appleSpawned = false;
    public HeadMovement headMove;

    [Header("Parameters")]
    public int scale;
    public (int, int) gridPos;

    [Header("Score")]
    public int score = 0;
    public int highScore = 0;
    public Boolean scoreChange = false;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;

    public GameObject scoreElements;

    [Header("Game Over")]
    public GameObject gameOverUI;

    public GameObject background;

    private Boolean gameOver = false;

    public Boolean gameRestart = false;

    [Header("Pausing")]
    public Boolean gamePaused;

    public GameObject gamePausedUI;



    void Update()
    {
        SpawnApple();
        PauseControl();
        background.SetActive(gamePaused);
        scoreElements.SetActive(!gamePaused);
        scoreDisplay.text = score.ToString();
        highScoreDisplay.text = highScore.ToString();
    }
    void Start()
    {
        InstantiatePlane();
        InstantiateWalls();
        setupGame();
    }

    private void setupGame() {
        background.SetActive(false);
        scoreElements.SetActive(true);
        gamePausedUI.SetActive(false);
        gameOverUI.SetActive(false);
        score = 0;
        gameRestart = false;
        gameOver = false;
        gamePaused = false;
        appleSpawned = false;
    }

    void SpawnApple()
    {
        if (!appleSpawned)
        {
            Boolean spawned = false;
            while (!spawned) {
                int x = UnityEngine.Random.Range(0, length) * scale * 10;
                int z = UnityEngine.Random.Range(0, length) * scale * 10;
                Vector3 posCord = new Vector3(x, 0, z);
                if (!headMove.snakeParts.Contains(posCord)) {
                    GameObject newGrid = Instantiate(apple, new Vector3(x, 1f, z), Quaternion.identity, transform);
                    spawned = true;
                }
            }
            
            appleSpawned = true;
        }
    }

    void InstantiateWalls() {
        for(int x = -1; x < length + 1; x++) {
            int actualX = x * scale * 10;
            Instantiate(wall, new Vector3(actualX, 5, -10f), Quaternion.identity, parent.transform);
            Instantiate(wall, new Vector3(actualX, 5, length * 10), Quaternion.identity, parent.transform);
        }
        for(int z = 0; z < length; z++) {
            int actualZ = z * scale * 10;
            Instantiate(wall, new Vector3(-10f, 5, actualZ), Quaternion.identity, parent.transform);
            Instantiate(wall, new Vector3(length * 10, 5, actualZ), Quaternion.identity, parent.transform);
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
        if (highScore < score) {
            highScore = score;
        }
        score = 0;
        gameOverUI.SetActive(true);
        gameOver = true;
    }

    public void PauseControl() {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver) { 
            //print(gamePaused);
            gamePaused = !gamePaused;
            //print(gamePaused);
            gamePausedUI.SetActive(gamePaused);
        }
    }

    public void QuitButton() {
        SceneManager.LoadScene(0);
    }

    public void RestartGame() {
        gameRestart = false;
        setupGame();
    }

}
