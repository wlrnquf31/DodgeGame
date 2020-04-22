﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public GameObject hpMenu;

    public GameObject stopMenu;

    public GameObject overMenu;

    public Text scoreText;

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
        overMenu.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Score : " + GameManager.instance.score;
    }

    public void StopUiController(bool isStop)
    {
        stopMenu.SetActive(isStop);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score;
    }
}
