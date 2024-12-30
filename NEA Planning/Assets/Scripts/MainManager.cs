using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    // Start is called before the first frame update

    // Now start w/ min values so the code for settting value doesn't break on first opening
    public int FoV = 60;
    public int volume = 0;

    public Slider fovSlider;
    public Slider volumeSlider;
    
    private Camera cam;
    private void Awake()
    {
         // Finds camera object in gameScene and sets its value to the FoV value
        cam = FindObjectOfType<Camera>();
        cam.fieldOfView = FoV;

        // Finds the instance of fovSlider and volumeSlider and sets these gameobjects to pointers in the sciprt
        fovSlider = GameObject.Find("FoV Slider").GetComponent<Slider>();
        volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();

        // Sets slider values to saved values, so settings save between scenes
        fovSlider.value = FoV;
        volumeSlider.value = volume;

        if (Instance != null)
        {
            print("Destorying new one created");
            Destroy(gameObject);
            return;
        }

        // Creats a version of MainManager, and sets it to not be destoryed on scene change
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void updateVolume() {
        volume = (int) volumeSlider.value;
    }
    public void updateFoV() {
        FoV = (int) fovSlider.value;
        //cam.fieldOfView = FoV;
    }


}
