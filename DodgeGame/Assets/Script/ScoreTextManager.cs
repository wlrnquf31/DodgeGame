using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    public void setScoreText(int score)
    {
        scoreText.text = "Score : " + score;
    }
}
