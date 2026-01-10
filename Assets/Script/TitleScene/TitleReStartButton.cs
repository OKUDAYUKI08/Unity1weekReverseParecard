using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleReStartButton : MonoBehaviour
{
    public void ReStart()
    {
        SceneManager.LoadScene("Title");
        parameter.instance.gameover = false;
    }
}
