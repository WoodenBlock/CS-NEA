using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject on;

    public GameObject off;

    private Boolean isOn = false;

    public void onButtonClick() {
        //print("button clicked");
        off.SetActive(isOn);
        on.SetActive(!isOn);
        isOn = !isOn;
    }
}
