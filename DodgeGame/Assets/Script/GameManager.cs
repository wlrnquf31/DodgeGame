using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public UiManager uiManager;

    public Player player;

    //public GameState currentGameState;

    public int score;
    //private readonly int ADD_SCORE = 100;

    private bool isStop = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
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

    private void Start()
    {
        //currentGameState = GameState.main;
        
    }

    //public void SetGameState(GameState gameState)
    //{
    //    currentGameState = gameState;
    //}

    public void ShieldHit()
    {
        score += DataManager.instance.Load().addScore;
        uiManager.SetScoreText(score);
        SoundManager.instance.PlayHitShieldSound();
    }

    public void PlayerHit()
    {
        player.Hp -= 1;
        uiManager.HpUiController();
        SoundManager.instance.PlayHitCharacterSound();
        if (player.Hp < 1)
        {
            GameOver();
            SoundManager.instance.BGMToggle();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        player.Hp = 2;
        score = 0;
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
        uiManager.StopUiController(isStop);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiManager.OverUiController();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
