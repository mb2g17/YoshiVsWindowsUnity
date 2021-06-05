using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on a level select button if we should only go
/// on this level after the true ending
/// </summary>
[RequireComponent(typeof(LevelSelectButtonScript))]
public class TrueEndingOnlyLevelScript : MonoBehaviour
{
    private LevelSelectButtonScript levelSelectButtonScript;

    /// <summary>
    /// Unknown sprite display
    /// </summary>
    public Sprite Unknown;

    // Start is called before the first frame update
    void Start()
    {
        levelSelectButtonScript = GetComponent<LevelSelectButtonScript>();

        // If we haven't gotten the true ending yet
        if (!GameState.Instance.GameData.GotTrueEnding)
        {
            levelSelectButtonScript.SetText("???");
            levelSelectButtonScript.SetSprite(Unknown);
            levelSelectButtonScript.EnableButton = false;
        }
    }
}
