using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour
{
    public TextMeshProUGUI MessageText;
    public TMP_InputField InputField;

    private AudioSource audioSource;

    public AudioClip CheatEnabledClip;
    public AudioClip WrongCheatClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// List of cheats with cheat name, cheat effect and cheat message
    /// </summary>
    private IDictionary<string, (Action, Func<string>)> cheats = new Dictionary<string, (Action, Func<string>)>()
    {
        { "lemonsqueezy", (() => {
            GameState.Instance.GameData.GotEnding = true;
            GameState.Instance.SaveGameData();
        }, () => { return "Normal ending completed!"; }) },

        { "vista", (() => {
            GameState.Instance.GameData.GotEnding = true;
            GameState.Instance.GameData.GotTrueEnding = true;
            GameState.Instance.SaveGameData();
        }, () => { return "Hard ending completed!"; }) },

        { "deathwish", (() => {
            GameState.Instance.GameData.GotEnding = true;
            GameState.Instance.GameData.GotTrueEnding = true;
            GameState.Instance.GameData.GotNightmareEnding = true;
            GameState.Instance.SaveGameData();
        }, () => { return "Nightmare ending completed!"; }) },

        { "nintendo", (() => {
            GameState.Instance.GodMode = !GameState.Instance.GodMode;
        }, () => { return GameState.Instance.GodMode ? "God mode enabled!" : "God mode disabled!"; }) },

        // --------------------

        { "gates", (() => {
            SceneManager.LoadScene("IconWorld");
        }, () => { return "Going to Icon World..."; }) },
        { "winblows", (() => {
            SceneManager.LoadScene("MonitorMountain");
        }, () => { return "Going to Monitor Mountain..."; }) },
        { "text", (() => {
            // Completes normal mode
            GameState.Instance.GameData.GotEnding = true;
            GameState.Instance.SaveGameData();

            SceneManager.LoadScene("LevelSelect");
        }, () => { return "Going to Level Select..."; }) },
    };

    public void SubmitCheat()
    {
        // Gets cheat text
        string cheatText = InputField.text;
        InputField.text = "";

        // If this cheat exists
        if (cheats.ContainsKey(cheatText))
        {
            // Fetch cheat
            (Action, Func<string>) cheat = cheats[cheatText];

            // Run cheat effect
            cheat.Item1();

            // Gets cheat message and displays it
            string message = cheat.Item2();
            MessageText.text = message;

            // Plays clip
            audioSource.PlayOneShot(CheatEnabledClip);
        }
        else
        {
            MessageText.text = "Invalid cheat.";

            // Plays clip
            audioSource.PlayOneShot(WrongCheatClip);
        }

        // Selects input field
        InputField.OnSelect(null);
    }
}
