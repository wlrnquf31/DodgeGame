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
    private readonly int ADD_SCORE = 100;

    private bool isStop = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //currentGameState = GameState.main;
        Time.timeScale = 1;
        player.Hp = 2;
        score = 0;
    }

    //public void SetGameState(GameState gameState)
    //{
    //    currentGameState = gameState;
    //}

    public void ShieldHit()
    {
        score += ADD_SCORE;
        uiManager.SetScoreText(score);
    }

    public void PlayerHit()
    {
        player.Hp -= 1;
        uiManager.HpUiController();
        if (player.Hp < 1)
        {
            GameOver();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameStop()
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
