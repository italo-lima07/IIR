using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gamecontroller : MonoBehaviour
{

    public Text healthText;

    public int score;
    public Text scoreText;
    
    public int totalScore;

    public static gamecontroller instance;

    public GameObject PAUSEoBJ;
    public GameObject GameOverobj;
    

    private bool IsPause;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        totalScore = PlayerPrefs.GetInt("score");
        Debug.Log(totalScore);
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        
        PlayerPrefs.SetInt("score",score + totalScore);
    }
    
    
    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPause = !IsPause;
            PAUSEoBJ.SetActive(IsPause);
        }

        if (IsPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        GameOverobj.SetActive(true);
    }

    
}
