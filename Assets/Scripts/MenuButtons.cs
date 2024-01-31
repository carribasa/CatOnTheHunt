using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("Boton pulsado");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
