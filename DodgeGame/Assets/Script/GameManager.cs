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

    public GameState currentGameState;

    public int score;

    public int hp;
    private const int MAX_HP = 2;

    public ScoreTextManager scoreTextManager;

    public GameObject hpMenu;

    public GameObject stopMenu;

    public GameObject overMenu;

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
        scoreTextManager.setScoreText(score);
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
        HpUiController();
    }

    private void HpUiController()
    {
        if (hpMenu != null)
        {
            for (int i = 0; i < MAX_HP; i++)
            {
                if (i <= hp - 1)
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

        stopMenu.SetActive(!stopMenu.activeInHierarchy);
    }

    public void GameOver()
    {
        overMenu.SetActive(true);
        Time.timeScale = 0;
        overMenu.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Score : " + score;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
