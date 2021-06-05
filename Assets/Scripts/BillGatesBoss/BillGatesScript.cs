using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillGatesScript : MonoBehaviour
{
    /// <summary>
    /// Private health
    /// </summary>
    private int health = 50;

    /// <summary>
    /// Health
    /// </summary>
    public int Health
    {
        get { return health; }
    }

    /// <summary>
    /// Health Bar ui image
    /// </summary>
    public Image HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        // Deducts health
        health = health == 0 ? 0 : health - 1;
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the health bar
    /// </summary>
    private void UpdateHealthBar()
    {
        HealthBar.fillAmount = (float)Health / 50f;
    }
}
