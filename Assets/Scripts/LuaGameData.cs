using Assets.Classes.Files;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to bring game data to lua scripts
/// </summary>
public class LuaGameData : MonoBehaviour
{
    /// <summary>
    /// Game data
    /// </summary>
    [HideInInspector]
    public GameData GameData
    {
        get
        {
            return GameState.Instance.GameData;
        }
    }

    /// <summary>
    /// Saves the game (can use this in Lua)
    /// </summary>
    public void SaveGameData()
    {
        GameState.Instance.SaveGameData();
    }
}
