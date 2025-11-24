using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    public void ClickingPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickingCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnStartMenut();
        }
    }

    public void ReturnStartMenut()
    {
        SceneManager.LoadScene(0);
    }
}
    
