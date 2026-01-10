using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingRule : MonoBehaviour
{
    public void RuleButton()
    {
        this.gameObject.SetActive(true);
    }
    public void ModoruButton()
    {
        this.gameObject.SetActive(false);
    }
}
