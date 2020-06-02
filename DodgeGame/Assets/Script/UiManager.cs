using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    public GameObject hpMenu;

    public GameObject stopMenu;

    public GameObject overMenu;

    public GameObject optionMenu;

    public GameObject changeMenu;

    public Text scoreText;

    public Text cashText;

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

    public void OverUiController()
    {
        overMenu.SetActive(true);
        overMenu.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = GameManager.instance.score.ToString();
        overMenu.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = DataManager.instance.Load().highScore.ToString();
    }

    public void OptionUiController()
    {
        optionMenu.SetActive(!optionMenu.activeInHierarchy);
    }

    public void SkinChangeUiController()
    {
        changeMenu.SetActive(!changeMenu.activeInHierarchy);
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

    public void ExitBtn()
    {
        GameManager.instance.GameExit();
    }
}
