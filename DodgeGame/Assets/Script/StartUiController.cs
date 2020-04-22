using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUiController : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionSetting()
    {

    }
}
