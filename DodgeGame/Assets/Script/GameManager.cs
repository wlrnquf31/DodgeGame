using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    main,
    inGame,
    gameStop,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public UiManager uiManager;

    public GameState currentGameState;

    public int score;

    public int hp;
    public const int MAX_HP = 2;

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
        currentGameState = GameState.main;
        score = 0;
        hp = 2;
    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        uiManager.setScoreText(score);
    }

    public void AddHp(int addHp)
    {
        hp += addHp;

        if(hp < 1)
        {
            GameOver();
        }
        else if(hp > MAX_HP)
        {
            hp = MAX_HP;
        }
        uiManager.HpUiController();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        AddHp(2);
        score = 0;
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

        uiManager.stopUiController(isStop);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiManager.overUiController();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
