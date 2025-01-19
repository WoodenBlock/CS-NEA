using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneratePlane : MonoBehaviour
{
    [Header("Area Generation")]
    public GameObject Plane;
    public GameObject Plane1;
    public GameObject Parent;
    public GameObject Wall;
    public int Length;

    [Header("Apple Spawning")]
    public GameObject Apple;
    public Boolean AppleSpawned = false;
    public HeadMovement HeadMove;

    [Header("Parameters")]
    public int Scale;

    [Header("Score")]
    public int Score = 0;
    public int HighScore = 0;
    public bool IsScoreChange = false;
    public TextMeshProUGUI ScoreDisplay;
    public TextMeshProUGUI HighScoreDisplay;

    public GameObject ScoreElements;

    [Header("Game Over")]
    public GameObject GameOverUI;

    public GameObject Background;

    private bool _isGameOver = false;

    [Header("Pausing")]
    public bool GamePaused = false;

    public GameObject GamePausedUI;



    void Update()
    {
        if (!AppleSpawned) {SpawnApple();}
        if (IsScoreChange) {
            // updates Score UI to new value
            ScoreDisplay.text = Score.ToString();
            // checks if new Score is a HighScore
            if (HighScore < Score) {
                HighScore = Score;
                GameObject.FindObjectOfType<MainManager>().UpdateHighscore(HighScore);
            }
            HighScoreDisplay.text = HighScore.ToString();
            // prevents Score from updated until its changed again
            IsScoreChange = false;
        }
        PauseControl();
        Background.SetActive(GamePaused);
        ScoreElements.SetActive(!GamePaused);
    }
    void Start()
    {
        InstantiatePlane();
        InstantiateWalls();
        SetupGame();
    }

    // resets all elements of the game scene to the correct place
    private void SetupGame() {
        Background.SetActive(false);
        ScoreElements.SetActive(true);
        GamePausedUI.SetActive(false);
        GameOverUI.SetActive(false);
        Score = 0;
        _isGameOver = false;
        GamePaused = false;
    }

    public void SpawnApple()
    {
        Boolean spawned = false;
        while (!spawned) {
            int x = UnityEngine.Random.Range(0, Length) * Scale * 10;
            int z = UnityEngine.Random.Range(0, Length) * Scale * 10;
            Vector3 posCord = new Vector3(x, 3, z);
            if (!HeadMove.SnakeParts.Contains(posCord)) {
                GameObject newGrid = Instantiate(Apple, new Vector3(x, 1f, z), Quaternion.identity, transform);
                spawned = true;
            }
        }
            
        AppleSpawned = true;
    }

    void InstantiateWalls() {
        // spawns walls around the planes 
        // spawns in two waves of two straight lines of walls either side of the plane
        for(int x = -1; x < Length + 1; x++) {
            int actualX = x * Scale * 10;
            Instantiate(Wall, new Vector3(actualX, 5, -10f), Quaternion.identity, Parent.transform);
            Instantiate(Wall, new Vector3(actualX, 5, Length * 10), Quaternion.identity, Parent.transform);
        }
        for(int z = 0; z < Length; z++) {
            int actualZ = z * Scale * 10;
            Instantiate(Wall, new Vector3(-10f, 5, actualZ), Quaternion.identity, Parent.transform);
            Instantiate(Wall, new Vector3(Length * 10, 5, actualZ), Quaternion.identity, Parent.transform);
        }
    }

    void InstantiatePlane()
    {
        for(int i = 0; i < Length * Length; i++)
        {
            // Comment
            int x = (i / Length) * Scale * 10;
            int z = (i % Length) * Scale * 10;
            if (i % 2 == 0)
            {
                Instantiate(Plane, new Vector3(x, 0, z), Quaternion.identity, Parent.transform);
            }
            else
            {
                Instantiate(Plane1, new Vector3(x, 0, z), Quaternion.identity, Parent.transform);
            }
        }
    }

    public void TriggerGameOver()
    {
        GamePaused = true;
        // Updates HighScore just in case
        if (HighScore < Score) {
            HighScore = Score;
        }
        // Resets Score back to 0
        Score = 0;
        IsScoreChange = true;
        GameOverUI.SetActive(true);
        _isGameOver = true;
    }

    public void PauseControl() {
        // toggles pause UI 
        if (Input.GetKeyDown(KeyCode.Escape) && !_isGameOver) { 
            GamePaused = !GamePaused;
            GamePausedUI.SetActive(GamePaused);
        }
    }

    public void QuitButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame() {
        SetupGame();
    }

}
