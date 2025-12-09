using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealtj : MonoBehaviour
{
    public List<RawImage> healthBar = new List<RawImage>();
    public Texture fullHealth;
    public Texture lostHealth;

    private int health;
    private HitPoints playerHitPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentHealth < health)
        {
            health = GameManager.Instance.currentHealth;
            healthBar[health].texture = lostHealth;
            Debug.Log(health);
        }
    }
}
