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

    private Coroutine cashCoroutine;

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

    private void OnApplicationQuit()
    {
        data = DataManager.instance.Load();
        DataManager.instance.Save(data);
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
        Time.timeScale = 1;
        score = 0;
        cashCoroutine = StartCoroutine(SpawnCash());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("GameScene"))
        {
            GameInit();
        }
    }

    private void GameOver()
    {
        StopCoroutine(cashCoroutine);
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
        //SceneLoadManager.LoadScene("GameScene");
        //GameInit();
    }

    public void GameReStart()
    {
        GameInit();
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
        GameData saveData = DataManager.instance.Load();

        DataUpdateCash(ref saveData);
        DataUpdateHighScore(ref saveData);

        DataManager.instance.Save(saveData);
    }

    private void DataUpdateCash(ref GameData saveData)
    {
        saveData.cash += gettingCash;
    }

    private void DataUpdateHighScore(ref GameData saveData)
    {
        if (score > data.highScore)
        {
            saveData.highScore = score;
        }
    }

}
