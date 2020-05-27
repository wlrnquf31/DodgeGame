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
    public Sprite skin;

    public GameObject cash;

    public int score;

    public int gettingCash;
    private readonly int addCash = 100;

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
        if(SceneManager.GetActiveScene().name.Equals("GameScene"))
        {
            player.GetComponent<SpriteRenderer>().sprite = skin;
            Time.timeScale = 1;
            player.Hp = 2;
            score = 0;
            StartCoroutine(SpawnCash());
        }
        else if(SceneManager.GetActiveScene().name.Equals("MainScene"))
        {
            UiManager.instance.SetCashText();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        FinishDataSave();
        UiManager.instance.OverUiController();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
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

            float randX = Random.Range(0, Screen.width);
            float randY = Random.Range(0, Screen.height);

            Vector3 randPos = Camera.main.ScreenToWorldPoint(new Vector3(randX, randY, 10));

            Instantiate(cash, randPos, Quaternion.identity);

        }
    }

    private void FinishDataSave()
    {
        data = DataManager.instance.Load();

        DataUpdateCash();
        DataUpdateHighScore();

        DataManager.instance.Save(data);
    }

    private void DataUpdateCash()
    {
        data.cash += gettingCash;

    }

    private void DataUpdateHighScore( )
    {
        if (score > data.highScore)
        {
            data.highScore = score;
        }

    }

}
