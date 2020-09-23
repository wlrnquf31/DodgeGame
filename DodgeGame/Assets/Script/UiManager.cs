using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    public GameObject hpMenu;

    public GameObject stopMenu;

    public GameObject overMenu;

    public GameObject optionMenu;

    public GameObject changeMenu;

    public GameObject closeUi;

    public Text scoreText;

    public Text cashText;

    public Text highScoreText;

    public Text gettingCashText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainScene"))
        {
            SetCashText();
            SetHighScoreText();
        }
    }

    public void HpUiController()
    {
        if (hpMenu != null)
        {
            for (int i = 0; i < GameManager.instance.player.MAX_HP ; i++)
            {
                if (i <= GameManager.instance.player.Hp - 1)
                {
                    hpMenu.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    hpMenu.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetJoyStickPosition()
    {
        GameData data = DataManager.instance.Load();

        bool isLeft = optionMenu.GetComponentInChildren<Toggle>().isOn;

        if (isLeft)
        {
            data.joystickIsLeft = true;
        }
        else
        {
            data.joystickIsLeft = false;
        }
        DataManager.instance.Save(data);
    }

    public void OverUiController()
    {
        overMenu.SetActive(true);
        overMenu.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = GameManager.instance.score.ToString();
        overMenu.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = DataManager.instance.Load().highScore.ToString();
    }

    public void OptionUiController()
    {
        if(optionMenu != null)
        {
            optionMenu.SetActive(!optionMenu.activeInHierarchy);
            optionMenu.GetComponentInChildren<Toggle>().isOn = DataManager.instance.Load().joystickIsLeft;
        }
    }

    public void CloseUiController()
    {
        if(closeUi != null)
        {
            closeUi.SetActive(!closeUi.activeInHierarchy);
        }
    }

    public void SkinChangeUiController()
    {
        if(changeMenu != null)
        {
            changeMenu.SetActive(!changeMenu.activeInHierarchy);
        }
    }

    public void StopUiController(bool isStop)
    {
        stopMenu.SetActive(isStop);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void SetCashText()
    {
        cashText.text = DataManager.instance.Load().cash.ToString();
    }

    public void SetHighScoreText()
    {
        highScoreText.text = DataManager.instance.Load().highScore.ToString();
    }

    public void SetGettingCashText()
    {
        gettingCashText.text = GameManager.instance.gettingCash.ToString();
    }

    public void GoGameSceneBtn()
    {
        GameManager.instance.GameStart();
    }

    public void GoMainSceneBtn()
    {
        GameManager.instance.GoToMain();
    }

    public void GameRestartBtn()
    {
        GameManager.instance.GameReStart();
    }

    public void BackToMainBtn()
    {
        GameManager.instance.BackToMain();
    }

    public void ExitBtn()
    {
        GameManager.instance.GameExit();
    }
}
