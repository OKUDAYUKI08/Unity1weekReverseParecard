using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmSliderController : MonoBehaviour
{
    public Slider slider;

    // Update is called once per frame
    void Start()
    {
        slider.value = BgmController.instance.BgmVolume;
    }
    void Update()
    {
        BgmController.instance.BgmVolume = slider.value;
    }
}
