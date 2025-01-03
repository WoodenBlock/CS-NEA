using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    private MainManager mainManager;
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    public void updateFov() {
        mainManager.updateFoV();
    }

    public void updateVolume() {
        mainManager.updateVolume();
    }

}
