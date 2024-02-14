using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    int index;

    void Update()
    {

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
