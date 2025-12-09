using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    [SerializeField]
    private TextMeshProUGUI scoreText = null;
    private int scoreNumber;

    [SerializeField]
    private TextMeshProUGUI snakeText = null;
    private int snakeCount;



    private void Start()
    {
        //ssnakeCount = AsteroidManager.Instance.stringCount;
        scoreNumber = AsteroidManager.asteroidScore;
        // display final score from previous game
        scoreText.text = "HighScore: " + scoreNumber.ToString();
        snakeText.text = "Largest Snake: " + snakeCount.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
