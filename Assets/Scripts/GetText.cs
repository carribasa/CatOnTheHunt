using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private void Awake()
    {
        TextItem myText = GameManagerSingleton.Instance.GetText(this.key);

        if (myText != null)
        {
            GetComponent<TMP_Text>().text = myText.value;
        }
    }
}
