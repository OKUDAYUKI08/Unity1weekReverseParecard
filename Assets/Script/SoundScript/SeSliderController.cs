using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeSliderController : MonoBehaviour
{
    public Slider slider;

    // Update is called once per frame
    void Start()
    {
        slider.value = parameter.instance.SeVolume;
    }
    void Update()
    {
        parameter.instance.SeVolume = slider.value;
    }
}
