using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using unityroom.Api;

public class GiveupButton : MonoBehaviour
{
    public resultactivecontroller resultactivecontroller;
 public void Giveup()
 {
    Debug.Log("ゲームオーバー");
    parameter.instance.gameover = true;
    resultactivecontroller.ResultActive();
    UnityroomApiClient.Instance.SendScore(1, parameter.instance.EnemyDefeats, ScoreboardWriteMode.HighScoreDesc);
 }
}
