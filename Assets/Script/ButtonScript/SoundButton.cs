using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public GameObject SoundView;
    public void SoundSettingButton()
    {
        SoundView.SetActive(true);
        parameter.instance.soundsetting = true;

    }
    public void BackButton()
    {
        SoundView.SetActive(false);
        parameter.instance.soundsetting = false;
    }
}
