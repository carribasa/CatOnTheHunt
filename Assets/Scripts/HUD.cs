using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnHurt.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
