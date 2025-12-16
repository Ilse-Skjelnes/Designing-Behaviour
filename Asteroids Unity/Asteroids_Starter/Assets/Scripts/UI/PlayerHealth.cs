using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public List<RawImage> healthBar = new List<RawImage>();
    public Texture fullHealth;
    public Texture lostHealth;

    private int health;
    private HitPoints playerHitPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if ((0 <= GameManager.Instance.currentHealth) && (GameManager.Instance.currentHealth < health))
        {
            int h = GameManager.Instance.currentHealth;
            healthBar[h].texture = lostHealth;
            Debug.Log(h);
            health = h;
        }
    }
}
