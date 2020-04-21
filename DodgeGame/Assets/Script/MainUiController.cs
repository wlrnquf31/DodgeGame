using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    public void startBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}
