using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialFeaturesButtonsEvents : MonoBehaviour
{
    public void LevelSelectOnPress()
    {
        if (GameState.Instance.GameData.GotEnding)
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void ExtraLevelsOnPress()
    {
        if (GameState.Instance.GameData.GotTrueEnding)
        {
            SceneManager.LoadScene("ExtraLevels");
        }
    }

    public void CustomYoshiColoursOnPress()
    {
        if (GameState.Instance.GameData.GotNightmareEnding)
        {
            SceneManager.LoadScene("CustomYoshiColours");
        }
    }
}
