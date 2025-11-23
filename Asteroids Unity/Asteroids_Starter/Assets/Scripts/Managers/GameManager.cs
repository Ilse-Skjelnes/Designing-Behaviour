using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GM's singleton for easy access throughout the whole project
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public static int finalScore = 0;

    [SerializeField]
    private TextMeshProUGUI scoreTextObject = null; 

    private int score = 0;

    [SerializeField]
    private GameObject player = null;
    public GameObject Player { get { return player; } }

    private void Awake()
    {
        // setup singleton
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }
    public void NotifyPlayerDeath()
    {
        // save final score then go to game over screen
        finalScore = score;
        SceneManager.LoadScene("GameOver");
    }

    public void AddScore(int amount)
    {
        // increase score and update UI
        score += amount;

        if (scoreTextObject != null)
            scoreTextObject.text = "Score: " + score.ToString();
    }
}
