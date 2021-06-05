using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthTextScript : MonoBehaviour
{
    /// <summary>
    /// The text GUI
    /// </summary>
    private TextMeshProUGUI _textGUI;

    /// <summary>
    /// Yoshi hurt clip
    /// </summary>
    public AudioClip HurtSound;

    /// <summary>
    /// If true, we will use the custom death scene instead of the GameOver scene
    /// </summary>
    public bool UseCustomDeathScene;

    /// <summary>
    /// The custom death scene to use
    /// </summary>
    public string CustomDeathScene;

    // Start is called before the first frame update
    void Start()
    {
        // Gets text component
        _textGUI = GetComponent<TextMeshProUGUI>();

        // If we're on nightmare mode, disable the text altogether
        if (GameState.Instance.GameData.Difficulty == Assets.Classes.Difficulty.NIGHTMARE)
            _textGUI.enabled = false;

        // Set action
        GameState.Instance.OnHealthChange = (oldHealth, newHealth) =>
        {
            // Sets text to health
            _textGUI.text = GameState.Instance.Health + "";

            // If Yoshi is dead
            if (newHealth == 0)
            {
                // Go to game over scene
                SceneManager.LoadScene(UseCustomDeathScene ? CustomDeathScene : "GameOver");
            }
        };

        // Updates GUI
        GameState.Instance.OnHealthChange(100, 100);
    }

    /// <summary>
    /// Deducts health from Yoshi
    /// </summary>
    /// <param name="damage">How much health to take off Yoshi</param>
    public void DeductHealth(int damage)
    {
        // If we're on nightmare, die instantly
        if (GameState.Instance.GameData.Difficulty == Assets.Classes.Difficulty.NIGHTMARE)
            GameState.Instance.Health = 0;
        else
        {
            // Play hurt sound
            FindObjectOfType<AudioSource>().PlayOneShot(HurtSound);

            int newHealth = GameState.Instance.Health - damage; // Gets new health value
            newHealth = newHealth < 0 ? 0 : newHealth; // Ensures we don't go negative
            GameState.Instance.Health = newHealth; // Updates health value in state
        }
    }
}
