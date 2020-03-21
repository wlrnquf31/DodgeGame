using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameState currentGameState;

    public int score;

    public ScoreTextManager scoreTextManager;

    public GameObject stopMenu;

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
    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
    }

    public void setScore(int currentScore)
    {
        score = currentScore;
        scoreTextManager.setScoreText(score);
    }

    public void StartGame()
    {

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

        stopMenu.SetActive(!stopMenu.activeInHierarchy);
    }

    public void GameOver()
    {

    }

    public void BackToMain()
    {

    }
}
