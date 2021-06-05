using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the object harmful to Yoshi if he touches it
/// </summary>
public class Harmful : MonoBehaviour
{
    /// <summary>
    /// The component responsible for Yoshi's health text GUI
    /// </summary>
    private HealthTextScript _healthTextScript;

    /// <summary>
    /// How much damage this does to Yoshi
    /// </summary>
    public int Attack = 2;

    /// <summary>
    /// The rate at which to harm Yoshi
    /// </summary>
    public float HarmRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Gets reference of Yoshi's health script
        _healthTextScript = FindObjectOfType<HealthTextScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the other thing is Yoshi AND we're enabled AND Yoshi is not invincible
        Yoshi yoshi = collision.gameObject.GetComponent<Yoshi>();
        if (yoshi != null && enabled && !yoshi.Invincible)
        {
            // Start harming Yoshi
            StartCoroutine("HarmYoshi");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the other thing is Yoshi AND we're enabled
        if (collision.gameObject.GetComponent<Yoshi>() != null && enabled)
        {
            // Stop harming Yoshi
            StopCoroutine("HarmYoshi");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the other thing is Yoshi AND we're enabled AND Yoshi is not invincible
        Yoshi yoshi = collision.gameObject.GetComponent<Yoshi>();
        if (yoshi != null && enabled && !yoshi.Invincible)
        {
            // Start harming Yoshi
            StartCoroutine("HarmYoshi");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the other thing is Yoshi AND we're enabled
        if (collision.gameObject.GetComponent<Yoshi>() != null && enabled)
        {
            // Stop harming Yoshi
            StopCoroutine("HarmYoshi");
        }
    }

    /// <summary>
    /// Coroutine that harms yoshi
    /// </summary>
    /// <returns></returns>
    private IEnumerator HarmYoshi()
    {
        while (true)
        {
            // If there's any attack
            if (Attack > 0)
            {
                // Harms Yoshi based on difficulty
                _healthTextScript.DeductHealth(
                    GameState.Instance.GameData.Difficulty == Assets.Classes.Difficulty.NORMAL ? Attack : Attack * 2);
            }
            yield return new WaitForSeconds(1f / HarmRate); // Waits a bit to harm him again
        }
    }
}
