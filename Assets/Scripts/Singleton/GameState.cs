using Assets.Classes;
using Assets.Classes.Files;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// State of the game
/// </summary>
public class GameState : Singleton<GameState>
{
    /// <summary>
    /// Game data
    /// </summary>
    private GameData gameData = new GameData(true, false, false, false, 100, Assets.Classes.Difficulty.NORMAL, "BillGatesBoss");
    public GameData GameData
    {
        get { return gameData; }
    }

    /// <summary>
    /// Yoshi health
    /// </summary>
    public int Health
    {
        get { return gameData.Health; }
        set
        {
            // If we're not on god mode
            if (!GodMode)
            {
                int oldHealth = gameData.Health; // Remembers old health value
                int newHealth = Mathf.Clamp(value, 0, 9999); // Stores new health, clamps it between 0 - anything

                gameData.Health = newHealth; // Update value
                OnHealthChange(oldHealth, newHealth); // Run event
            }
        }
    }

    /// <summary>
    /// God mode; if true, take no damage
    /// </summary>
    public bool GodMode = false;

    /// <summary>
    /// Sets the file to file 1 by default
    /// </summary>
    private void Start()
    {
        //SetGameData(1, FileManager.LoadFile(1));
    }

    /// <summary>
    /// The file number of the game data
    /// </summary>
    private int fileNo;
    public int FileNo
    {
        get
        {
            return fileNo;
        }
    }

    /// <summary>
    /// Sets game data on this singleton
    /// </summary>
    /// <param name="fileNo">The file number</param>
    /// <param name="gameData"></param>
    public void SetGameData(int fileNo, GameData gameData)
    {
        this.fileNo = fileNo;
        this.gameData = gameData;
    }

    /// <summary>
    /// Saves game data using file number
    /// </summary>
    public void SaveGameData()
    {
        FileManager.SaveFile(fileNo, GameData);
    }

    /// <summary>
    /// Event that is run when health value changes, with two parameters:
    /// oldHealth - the old health value
    /// newHealth - the new health value
    /// </summary>
    public Action<int, int> OnHealthChange = (oldHealth, newHealth) => { };

    /// <summary>
    /// Prevent non-singleton constructor use.
    /// </summary>
    protected GameState() { }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameState))]
public class GameStateEditor : Editor
{
    private SerializedProperty _health;

    private void OnEnable()
    {
        _health = serializedObject.FindProperty("Health");
    }

    public override void OnInspectorGUI()
    {
        /*serializedObject.Update();

        EditorGUILayout.PropertyField(_health);

        serializedObject.ApplyModifiedProperties();*/

        GameState gameState = (GameState)target;

        gameState.Health = EditorGUILayout.IntField("Health", gameState.Health);
    }
}
#endif