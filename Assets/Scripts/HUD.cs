using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] RectTransform health;

    void Start()
    {
        this.health = this.transform.GetComponent<RectTransform>();
        GameManager.Instance.OnHurt.AddListener(LoseHealth);
    }

    public void LoseHealth()
    {
        int lives = GameManager.Instance.Lives;

        health.sizeDelta = new Vector2(46.5f, health.sizeDelta.y);


    }
}
