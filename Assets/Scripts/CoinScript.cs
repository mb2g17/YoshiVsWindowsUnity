using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    /// <summary>
    /// Sound that plays when you collect a coin
    /// </summary>
    public AudioClip CoinSound;

    /// <summary>
    /// If true, this coin will take Yoshi back to 100
    /// </summary>
    public bool BackTo100 = true;

    /// <summary>
    /// If this won't take Yoshi back to 100, health will go up by this amount
    /// </summary>
    public int AmountToHeal = 5;

    /// <summary>
    /// If true, we are hidden
    /// </summary>
    [HideInInspector]
    public bool Hidden = false;

    /// <summary>
    /// On Destroy action
    /// </summary>
    [HideInInspector]
    public Action OnDestroy = () => { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Heals
            if (BackTo100)
                GameState.Instance.Health = Mathf.Clamp(GameState.Instance.Health, 100, 9999);
            else
                GameState.Instance.Health += AmountToHeal;

            // Plays coin sound
            GetComponent<AudioSource>().PlayOneShot(CoinSound);

            // Hides self
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Sets to hidden
            Hidden = true;

            // Wait a second before dying
            StartCoroutine("DieCoroutine");
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2);
        OnDestroy();
        Destroy(gameObject);
    }
}
