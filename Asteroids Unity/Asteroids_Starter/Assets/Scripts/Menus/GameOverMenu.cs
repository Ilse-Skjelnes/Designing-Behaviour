using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    [SerializeField]
    private TextMeshProUGUI scoreText = null;

    private void Start()
    {
        // display final score from previous game
        scoreText.text = "Score: " + GameManager.finalScore.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
