using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource music;
    public bool start;
    public BeatScroller scroller;
    public static GameManager instance;
    public int currentScore;
    public int perfectScore = 2;
    public int goodScore = 1;
    public int missed = 1;
    public TextMeshProUGUI scoreText;
    public bool gameIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score : 0";
        gameIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.anyKeyDown)
            {
                start = true;
                scroller.hasStarted = true;

                music.Play();
            }
        }

        if (currentScore < 0 && gameIsRunning)
        {
            /*StopTime();*/
        }
    }

    public void PerfectHit()
    {
        Debug.Log("Perfect");


        currentScore += perfectScore;
        scoreText.text = "Score : " + currentScore;
    }

    public void GoodHit()
    {
        Debug.Log("Good");

        currentScore += goodScore;
        scoreText.text = "Score : " + currentScore;
    }

    public void NoteMiss()
    {
        Debug.Log("Missed");

        currentScore -= missed;
        scoreText.text = "Score : " + currentScore;
    }

    //public void StopTime()
    //{
    //    Debug.Log("Game has stopped due to score less than 0");

    //    gameIsRunning = false;
    //    music.Pause();
    //    Time.timeScale = 0;
    //}
}
