using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script where, if the player reaches this script, they have completed
/// an ending
/// </summary>
public class EndingScript : MonoBehaviour
{
    private void Start()
    {
        // Depending on ending, grant player a flag
        switch (GameState.Instance.GameData.Difficulty)
        {
            case Difficulty.NORMAL:
                GameState.Instance.GameData.GotEnding = true;
                break;
            case Difficulty.HARD:
                GameState.Instance.GameData.GotEnding = true;
                GameState.Instance.GameData.GotTrueEnding = true;
                break;
            case Difficulty.NIGHTMARE:
                GameState.Instance.GameData.GotEnding = true;
                GameState.Instance.GameData.GotTrueEnding = true;
                GameState.Instance.GameData.GotNightmareEnding = true;
                break;
        }

        // Saves the game
        GameState.Instance.SaveGameData();

        // Destroys this object
        Destroy(gameObject);
    }
}
