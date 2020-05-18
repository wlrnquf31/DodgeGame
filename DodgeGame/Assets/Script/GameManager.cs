using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using DataInfo;

//public enum GameState
//{
//    main,
//    inGame,
//    gameStop,
//    gameOver
//}

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private GameData data = new GameData();

    public Player player;

    public int score;

    private bool isStop = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameInit();
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

    private void OnApplicationQuit()
    {
        DataManager.instance.Save(DataManager.instance.Load());
    }

    public void ShieldHit()
    {
        score += DataManager.instance.Load().addScore;
        UiManager.instance.SetScoreText(score);
        SoundManager.instance.PlayHitShieldSound();
    }

    public void PlayerHit()
    {
        player.Hp -= 1;
        UiManager.instance.HpUiController();
        SoundManager.instance.PlayHitCharacterSound();
        if (player.Hp < 1)
        {
            GameOver();
            SoundManager.instance.BGMToggle();
        }
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
        if(SceneManager.GetActiveScene().name.Equals("GameScene"))
        {
            Time.timeScale = 1;
            player.Hp = 2;
            score = 0;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        HighScoreCheck();
        UiManager.instance.OverUiController();
    }

    private bool HighScoreCheck()
    {
        data = DataManager.instance.Load();

        if (score > data.highScore)
        {
            data.highScore = score;
            DataManager.instance.Save(data);

            return true;
        }

        return false;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
