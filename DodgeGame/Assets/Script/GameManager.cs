﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using DataInfo;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private GameData data = new GameData();

    public Player player;

    public Sprite skin;

    public GameObject cash;

    public int score;

    public int gettingCash;

    private readonly int addCash = 100;

    private bool isStop = false;

    private bool isTwiceCoin = false;

    private bool isTwiceScore = false;

    private Coroutine cashCoroutine;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationPause(bool pause)
    {
        if(SceneManager.GetActiveScene().name.Equals("GameScene"))
        {
            if (!isStop)
            {
                GamePlayToggle();
            }
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("GameScene"))
        {
            GameInit();
        }
        else if(scene.name.Equals("MainScene"))
        {
            StartCoroutine(CheckEscape());
        }
    }

    private void OnApplicationQuit()
    {
        data = DataManager.instance.Load();
        DataManager.instance.Save(data);
    }

    IEnumerator CheckEscape()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UiManager.instance.CloseUiController();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void ScoreUp()
    {
        if(isTwiceScore)
        {
            score += DataManager.instance.Load().addScore * 2;
        }
        else
        {
            score += DataManager.instance.Load().addScore;
        }
        UiManager.instance.SetScoreText(score);
    }

    public void CashHit()
    {
        gettingCash += addCash;
        UiManager.instance.SetGettingCashText();
        SoundManager.instance.PlayPickUpSound();
    }

    public void GamePlayToggle()
    {

        if (isStop)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        isStop = !isStop;
        SoundManager.instance.BGMToggle();
        UiManager.instance.StopUiController(isStop);
    }

    public void GameInit()
    {
        int[] usedBoost = new int[4] { 1, 1, 1, 1 };

        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        Time.timeScale = 1;
        gettingCash = 0;
        score = 0;
        isTwiceScore = false;
        isTwiceCoin = false;
        UseBoost(usedBoost);
        cashCoroutine = StartCoroutine(SpawnCash());
    }

    private void UseBoost(int[] usedBoost)
    {
        data = DataManager.instance.Load();

        for(int i = 0; i < data.keepBoost.Length; i++)
        {
            if(usedBoost[i] != 0 && data.keepBoost[i] != 0)
            {
                data.keepBoost[i]--;
                DataManager.instance.Save(data);
                StartCoroutine(ApplyBoost(i));
            }
        }
    }

    IEnumerator ApplyBoost(int boostIndex)
    {
        if(boostIndex.Equals(0))
        {
            isTwiceScore = true;
            yield return new WaitForSeconds(30f);
            isTwiceScore = false;
        }
        else if(boostIndex.Equals(1))
        {
            player.isGodMode = true;
            yield return new WaitForSeconds(10f);
            player.isGodMode = false;
        }
        else if(boostIndex.Equals(2))
        {
            player.isOneIgnore = true;
        }
        else if(boostIndex.Equals(3))
        {
            isTwiceCoin = true;
        }
        
    }

    public void GameOver()
    {
        UiManager.instance.OverUiController();
        Time.timeScale = 0;
    }

    public void GameFinish()
    {
        FinishDataSave();
        StopCoroutine(cashCoroutine);
        SoundManager.instance.BGMToggle();
    }

    public void GameReStart()
    {
        GameFinish();
        GameStart();
        //GameInit();
    }

    public void BackToMain()
    {
        GameFinish();
        GoToMain();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        StopCoroutine("CheckEscape");
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator SpawnCash()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);

            float randX = Random.Range(player.halfSizeX * 100 , Screen.width - (player.halfSizeX * 100));
            float randY = Random.Range(player.halfSizeY * 100 , Screen.height - (player.halfSizeY * 100));

            Vector3 randPos = Camera.main.ScreenToWorldPoint(new Vector3(randX, randY, 10));

            Instantiate(cash, randPos, Quaternion.identity);

        }
    }

    private void FinishDataSave()
    {
        GameData saveData = DataManager.instance.Load();

        DataUpdateCash(ref saveData);
        DataUpdateHighScore(ref saveData);

        DataManager.instance.Save(saveData);
    }

    private void DataUpdateCash(ref GameData saveData)
    {
        if(isTwiceCoin)
        {
            saveData.cash += (gettingCash * 2);
            isTwiceCoin = false;
        }
        else
        {
            saveData.cash += gettingCash;
        }
    }

    private void DataUpdateHighScore(ref GameData saveData)
    {
        if (score > data.highScore)
        {
            saveData.highScore = score;
        }
    }

}
