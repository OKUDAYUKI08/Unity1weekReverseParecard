using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameStartButton : MonoBehaviour
{
    public void MainGameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
}
