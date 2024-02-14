using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int Lives = 3;
    public UnityEvent OnHurt = new UnityEvent();
    public UnityEvent OnHeal = new UnityEvent();
    public UnityEvent OnPlay = new UnityEvent();
    public UnityEvent OnPause = new UnityEvent();

    /*
     * Variable p�blica que devolver� el contenido de instacia, si existe o 
     * crear� el Game Manager -> SetupInstance
     * */
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                SetupInstance();
            }
            return instance;
        }
    }

    void Awake()
    {

    }

    // Iniciar GAME MANAGER
    private static void SetupInstance()
    {
        instance = FindObjectOfType<GameManager>();
        if (instance == null)
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "GameManager";
            instance = gameObject.AddComponent<GameManager>();

            // No destruir a lo largo de la ejecucion
            DontDestroyOnLoad(gameObject);
        }
    }

    public TextItem GetText(string key)
    {
        // Buscamos en un diccionario la clave, si existe, devolvemos su valor
        var json = Resources.Load("es");
        Texts myTexts = JsonUtility.FromJson<Texts>(json.ToString());
        TextItem myText = myTexts.items.Where(x => x.key == key).FirstOrDefault();

        if (myText != null)
        {
            Debug.Log(myText);
            return myText;
        }
        return myText;
    }

}

[Serializable]
public class Texts
{
    public List<TextItem> items;

}

[Serializable]
public class TextItem
{
    public string key;
    public string value;
}
