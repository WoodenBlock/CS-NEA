using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject on;

    public GameObject off;

    public Boolean isOn = false;

    // Makes sure button displays correct values when scene is reloaded
    public void ButtonStartup(Boolean value) {
        isOn = value;
        off.SetActive(!isOn);
        on.SetActive(isOn);
    }
    public void onButtonClick() {
        off.SetActive(isOn);
        on.SetActive(!isOn);
        isOn = !isOn;
        print(isOn);
    }
}
