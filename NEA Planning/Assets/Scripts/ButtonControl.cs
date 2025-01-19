using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject on;

    public GameObject off;

    public Boolean isOn = false;

    // Makes sure button displays correct values when scene is loaded
    public void ButtonStartup(Boolean value) {
        isOn = value;
        off.SetActive(!isOn);
        on.SetActive(isOn);
    }
    // Swaps button states on click
    public void onButtonClick() {
        off.SetActive(isOn);
        on.SetActive(!isOn);
        isOn = !isOn;
    }
}
