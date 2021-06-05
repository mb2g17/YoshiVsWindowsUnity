using Assets.Classes;
using Assets.Classes.Files;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyPicker : MonoBehaviour
{
    /// <summary>
    /// Different kinds of sprites
    /// </summary>
    public Sprite NormalButton, HardButton, NightmareButton;

    /// <summary>
    /// Image to update
    /// </summary>
    public Image ButtonImage;

    /// <summary>
    /// When true, limits difficulties based on playthroughs
    /// </summary>
    public bool LimitDifficulties = true;

    /// <summary>
    /// Current difficulty set
    /// </summary>
    private Difficulty difficulty = Difficulty.NORMAL;

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonImage();
    }

    public Difficulty GetDifficulty()
    {
        return difficulty;
    }

    /// <summary>
    /// Updates button image
    /// </summary>
    public void UpdateButtonImage()
    {
        switch (difficulty)
        {
            case Difficulty.NORMAL:
                ButtonImage.sprite = NormalButton;
                break;
            case Difficulty.HARD:
                ButtonImage.sprite = HardButton;
                break;
            case Difficulty.NIGHTMARE:
                ButtonImage.sprite = NightmareButton;
                break;
        }
    }

    /// <summary>
    /// Scrolls through difficulties
    /// </summary>
    public void ScrollDifficulty()
    {
        GameData gameData = GameState.Instance.GameData;

        switch (difficulty)
        {
            case Difficulty.NORMAL:
                if (!LimitDifficulties || gameData.GotTrueEnding)
                    difficulty = Difficulty.HARD;
                break;
            case Difficulty.HARD:
                if (!LimitDifficulties || gameData.GotNightmareEnding)
                    difficulty = Difficulty.NIGHTMARE;
                else
                    difficulty = Difficulty.NORMAL;
                break;
            case Difficulty.NIGHTMARE:
                difficulty = Difficulty.NORMAL;
                break;
        }
    }

    /// <summary>
    /// When the button is clicked
    /// </summary>
    public void OnDifficultyClick()
    {
        ScrollDifficulty();
        UpdateButtonImage();
    }
}
