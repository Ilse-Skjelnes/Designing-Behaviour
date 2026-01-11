using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    [SerializeField]
    private TextMeshProUGUI snakeText = null;
    private int snakeCount;



    private void Start()
    {
        snakeCount = AsteroidManager.Instance.snakeCount;
        // display final score from previous game
        snakeText.text = snakeCount.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
