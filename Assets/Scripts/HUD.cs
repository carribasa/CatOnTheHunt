using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image heartImage;
    [SerializeField] RectTransform health;
    [SerializeField] TMP_Text pointsHUD;
    int lives;
    [SerializeField] Player player;
    float healthWidth = 48f;

    void Start()
    {
        lives = GameManager.Instance.Lives;
        health = heartImage.GetComponent<RectTransform>();
        GameManager.Instance.OnHurt.AddListener(LoseHealth);
        GameManager.Instance.OnHeal.AddListener(RecoverLife);
        GameManager.Instance.OnHitPoint.AddListener(AddPoint);
        health.sizeDelta = new Vector2(healthWidth * lives, health.sizeDelta.y);
        pointsHUD.text = GameManager.Instance.Points.ToString();
    }

    public void LoseHealth()
    {
        health.sizeDelta = new Vector2(healthWidth * player.lives, health.sizeDelta.y);
    }

    public void RecoverLife()
    {
        int lives = GameManager.Instance.Lives;
        health.sizeDelta = new Vector2(healthWidth * player.lives, health.sizeDelta.y);
    }

    public void AddPoint()
    {
        int points = GameManager.Instance.Points;
        pointsHUD.text = points.ToString();
    }
}
