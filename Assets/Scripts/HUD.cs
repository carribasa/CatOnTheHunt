using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image heartImage;
    [SerializeField] RectTransform health;
    int lives;
    float healthWidth = 48f;

    void Start()
    {
        lives = GameManager.Instance.Lives;
        health = heartImage.GetComponent<RectTransform>();
        GameManager.Instance.OnHurt.AddListener(LoseHealth);
        health.sizeDelta = new Vector2(healthWidth * lives, health.sizeDelta.y);
        
    }

    public void LoseHealth()
    {
        int lives = GameManager.Instance.Lives;
        health.sizeDelta = new Vector2(healthWidth * lives, health.sizeDelta.y);
    }
}
