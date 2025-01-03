using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    // Start is called before the first frame update

    // Now start w/ min values so the code for settting value doesn't break on first opening
    public int fieldOfView = 60;
    public int volume = 100;

    public GameObject settingsMenu;
    public Slider fovSlider;
    public Slider volumeSlider;
    private int oldScene = 0;
    
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

        if (SceneManager.GetActiveScene().buildIndex == 1 && oldScene == 0) {
            oldScene = 1;
            // Finds camera object in gameScene and sets its value to the FoV value
            cam = FindObjectOfType<Camera>();
            cam.fieldOfView = fieldOfView;

            eatingSource = FindObjectOfType<AudioSource>();
            eatingSource.volume = volume / 100;

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

    // Function to activate the effcts of the activated modifiers
    private void ActivateModifiers() {
        if (twoApples) {
            gameManager = FindObjectOfType<GeneratePlane>();
            gameManager.SpawnApple();
            gameManager.SpawnApple();
        }
        if (fasterSpeed) {
            snakeManager = FindObjectOfType<HeadMovement>();
            snakeManager.baseSpeed = 100;
            snakeManager.maxSpeed = 170;
        }
    }

    // Funcitons finds the instances of the gameObjects that the script maanges
    public void Setup() {

        // Finds the instance of fovSlider and volumeSlider and sets these gameobjects to pointers in the sciprt
        settingsMenu = GameObject.Find("/Canvas/SettingsMenu");
        volumeSlider = GameObject.Find("/Canvas/SettingsMenu/VolumeSlider").GetComponent<Slider>();
        fovSlider = GameObject.Find("/Canvas/SettingsMenu/FoVSlider").GetComponent<Slider>();
        RestoreModifiers();

        // Deactives settings menu to make sure it can't be seen
        settingsMenu.SetActive(false);

        // Sets slider values to saved values, so settings save between scenes
        fovSlider.value = fieldOfView;
        volumeSlider.value = volume;
    }

    public void updateVolume() {
        volume = (int) volumeSlider.value;
    }
    public void updateFoV() {
        fieldOfView = (int) fovSlider.value;
    }
}
