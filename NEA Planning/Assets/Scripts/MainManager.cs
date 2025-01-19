using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    // Start is called before the first frame update

    // Now start w/ min values so the code for settting value doesn't break on first opening
    public int fieldOfView = 60;
    public int volume = 100;

    public GameObject SettingsMenu;
    public Slider FovSlider;
    public Slider VolumeSlider;
    private int oldScene = 0;
    
    private int _highScore;
    private Camera cam;

    private AudioSource eatingSource;

    private GeneratePlane gameManager;

    private HeadMovement snakeManager;

    private Boolean twoApples = false;

    private Boolean fasterSpeed = false;
    private void Awake()
    {

        // Prevents another instance of the object being created by causing duplicates to be deleted on creation
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Creats a version of MainManager, and sets it to not be destoryed on scene change
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Setup();

    }

    public void Update() {
        // Checks if the scene has just changed to the game one
        if (SceneManager.GetActiveScene().buildIndex == 1 && oldScene == 0) {

            oldScene = 1;

            // Finds camera object in gameScene and sets its value to the FoV value
            cam = FindObjectOfType<Camera>();
            cam.fieldOfView = fieldOfView;

            // Find AudioSource and sets its value to the volume
            eatingSource = FindObjectOfType<AudioSource>();
            // AudioSources use 0-1, while slider used 0-100, so / 100 converts
            eatingSource.volume = volume / 100;

            RestoreHighscore();

            ActivateModifiers();

        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && oldScene == 1) {
            oldScene = 0;
            Setup();
        }
    }

    // Function to get all modifier values. Called by onPress of modifiers through the input manager intermediate object
    public void UpdateModifiers() {
        twoApples = GameObject.Find("Two Apples").GetComponent<ButtonControl>().isOn;
        fasterSpeed = GameObject.Find("Faster Starting Speed").GetComponent<ButtonControl>().isOn;
    }

    // Function that updates modifiers with saved values between scenes
    public void RestoreModifiers() {
        GameObject.Find("Two Apples").GetComponent<ButtonControl>().ButtonStartup(twoApples);
        GameObject.Find("Faster Starting Speed").GetComponent<ButtonControl>().ButtonStartup(fasterSpeed);
    }

    // Callled by GeneratePlane to update highscore stored
    public void UpdateHighscore(int newScore) {
        _highScore = newScore;
    }

    // Used to restore previous highscore
    private void RestoreHighscore() {
        GameObject.Find("High Score Display").GetComponent<TextMeshProUGUI>().text = _highScore.ToString();
        GameObject.FindObjectOfType<GeneratePlane>().HighScore = _highScore;
    }

    // Function to activate the effcts of the activated modifiers
    private void ActivateModifiers() {
        if (twoApples) {
            gameManager = FindObjectOfType<GeneratePlane>();
            gameManager.SpawnApple();
            gameManager.SpawnApple();
        }
        if (fasterSpeed) {
            snakeManager = FindObjectOfType<HeadMovement>();
            snakeManager.BaseSpeed = 100;
            snakeManager.MaxSpeed = 170;
        }
    }

    // Funcitons finds the instances of the gameObjects that the script maanges
    public void Setup() {

        // Finds the instance of FovSlider and VolumeSlider and sets these gameobjects to pointers in the sciprt
        SettingsMenu = GameObject.Find("/Canvas/SettingsMenu");
        VolumeSlider = GameObject.Find("/Canvas/SettingsMenu/VolumeSlider").GetComponent<Slider>();
        FovSlider = GameObject.Find("/Canvas/SettingsMenu/FoVSlider").GetComponent<Slider>();
        RestoreModifiers();

        // Deactives settings menu to make sure it can't be seen
        SettingsMenu.SetActive(false);

        // Sets slider values to saved values, so settings save between scenes
        FovSlider.value = fieldOfView;
        VolumeSlider.value = volume;
    }

    public void updateVolume() {
        volume = (int) VolumeSlider.value;
    }
    public void updateFoV() {
        fieldOfView = (int) FovSlider.value;
    }
}
