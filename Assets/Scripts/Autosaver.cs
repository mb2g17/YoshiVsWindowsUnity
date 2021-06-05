using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used in game levels; saves the game at a specific level, then destroys self
/// </summary>
public class Autosaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.GameData.AreWeDoingPlaythrough = true;
        GameState.Instance.GameData.Level = SceneManager.GetActiveScene().name;
        GameState.Instance.SaveGameData();
        Destroy(gameObject);
    }
}
