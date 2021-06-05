using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTextScript : MonoBehaviour
{
    /// <summary>
    /// The text GUI
    /// </summary>
    private TextMeshProUGUI _textGUI;

    /// <summary>
    /// Yoshi win clip
    /// </summary>
    public AudioClip WinSound;

    /// <summary>
    /// Event that is run when all enemies die
    /// </summary>
    public Action OnAllEnemiesDead = () => { };

    // Start is called before the first frame update
    void Start()
    {
        // Gets text obj
        _textGUI = GetComponent<TextMeshProUGUI>();

        // Resets enemy count
        ResetCount();
    }

    /// <summary>
    /// Resets count of enemies by recounting
    /// </summary>
    public void ResetCount()
    {
        int noOfEnemies = FindObjectsOfType<Enemy>().Length;
        _textGUI.text = noOfEnemies + "";
    }

    /// <summary>
    /// Deducts the enemy count
    /// </summary>
    public void DeductCount()
    {
        // Gets the new enemy count
        int newEnemyCount = Convert.ToInt32(_textGUI.text) - 1;
        _textGUI.text = newEnemyCount + "";

        // If that was the last one
        if (newEnemyCount == 0)
        {
            // Runs event
            OnAllEnemiesDead();

            // Play win sound
            FindObjectOfType<AudioSource>().PlayOneShot(WinSound);

            // Take Yoshi out of any blocks (so he doesn't get deleted)
            FindObjectOfType<Yoshi>().gameObject.transform.parent = null;

            // Destroy the question blocks
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("QuestionBlock"))
                Destroy(obj);
        }
    }

    /// <summary>
    /// Gets the number of enemies
    /// </summary>
    /// <returns>Number of enemies</returns>
    public int GetEnemyCount()
    {
        return Convert.ToInt32(_textGUI.text);
    }
}
