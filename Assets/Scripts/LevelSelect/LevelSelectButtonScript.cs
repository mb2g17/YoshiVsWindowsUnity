using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour
{
    /// <summary>
    /// The name of the scene to display
    /// </summary>
    public string SceneName;

    /// <summary>
    /// A picture of the scene
    /// </summary>
    public Sprite SceneImage;

    /// <summary>
    /// Preview image
    /// </summary>
    public Image PreviewImage;

    /// <summary>
    /// Display text
    /// </summary>
    public TextMeshProUGUI Text;

    /// <summary>
    /// Difficulty picker
    /// </summary>
    public DifficultyPicker DifficultyPicker;

    /// <summary>
    /// If false, we can't click on this button
    /// </summary>
    [HideInInspector]
    public bool EnableButton = true;

    void Awake()
    {
        SetText(CamelCaseSplitter.SplitCamelCase(SceneName));
        SetSprite(SceneImage);
    }

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void SetSprite(Sprite sprite)
    {
        PreviewImage.sprite = sprite;
    }

    public void OnClick()
    {
        if (EnableButton)
        {
            // Sets difficulty
            GameState.Instance.GameData.Difficulty = DifficultyPicker.GetDifficulty();

            // Loads level
            SceneManager.LoadScene(SceneName);
        }
    }
}
