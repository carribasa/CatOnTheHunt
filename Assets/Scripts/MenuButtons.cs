using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    [SerializeField] int numDeaths;

    public void StartNewGameEasy()
    {
        GameManager.Instance.Lives = 3;
        GameManager.Instance.hardMode = false;
        SceneManager.LoadScene("Level1");
    }

    public void StartNewGameHard()
    {
        GameManager.Instance.Lives = 1;
        GameManager.Instance.hardMode = true;
        SceneManager.LoadScene("Level1");
    }

    public void RestartLevel()
    {
        if (GameManager.Instance.hardMode)
        {
            GameManager.Instance.Lives = 1;
            SceneManager.LoadScene("Level1");
        }
        else
        {
            if (GameManager.Instance.NumDeaths > 3)
            {
                GameManager.Instance.Lives = 5;
                SceneManager.LoadScene("Level1");
            }
            else
            {
                GameManager.Instance.Lives = 3;
                SceneManager.LoadScene("Level1");
            }
        }
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.NumDeaths = 0;
        SceneManager.LoadScene("Menu");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
