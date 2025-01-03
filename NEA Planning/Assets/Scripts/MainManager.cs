using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    // Start is called before the first frame update

    // Now start w/ min values so the code for settting value doesn't break on first opening
    public int fieldOfView = 60;
    public int volume = 0;

    public GameObject settingsMenu;
    public Slider fovSlider;
    public Slider volumeSlider;
    private int oldScene = 0;
    
    private Camera cam;

    private AudioSource eatingSource;
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
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 && oldScene == 1) {
            oldScene = 0;
            Setup();
        }
    }


    // Funcitons finds the instances of the gameObjects that the script maanges
    public void Setup() {

        // Finds the instance of fovSlider and volumeSlider and sets these gameobjects to pointers in the sciprt
        settingsMenu = GameObject.Find("/Canvas/SettingsMenu");
        volumeSlider = GameObject.Find("/Canvas/SettingsMenu/VolumeSlider").GetComponent<Slider>();
        fovSlider = GameObject.Find("/Canvas/SettingsMenu/FoVSlider").GetComponent<Slider>();

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
