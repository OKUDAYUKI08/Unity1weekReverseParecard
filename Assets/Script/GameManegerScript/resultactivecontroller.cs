using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resultactivecontroller : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public GameObject resultview;
    void Update()
    {
        Score.text ="";
        Score.text +=$"{parameter.instance.EnemyDefeats}\n";
        Score.text +=$"{parameter.instance.score}\n";
        Score.text +=$"{parameter.instance.misscount}\n";
        Score.text +=$"{parameter.instance.Player_Attack}\n";
        Score.text +=$"{parameter.instance.Player_MaxHp}\n";
    }
    void Start()
    {
        resultview.SetActive(false);
    }
    public void ResultActive()
    {
        resultview.SetActive(true);
    }
}
