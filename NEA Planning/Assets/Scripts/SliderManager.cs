using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    private MainManager _mainManager;
    void Start()
    {
        // links itself to main manager
        _mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // functions to get main manager to get new updated values
    // called by the onPress and onChanged functions
    public void updateFov() {
        _mainManager.updateFoV();
    }

    public void updateVolume() {
        _mainManager.updateVolume();
    }

    public void UpdateModifiers() {
        _mainManager.UpdateModifiers();
    }

}
