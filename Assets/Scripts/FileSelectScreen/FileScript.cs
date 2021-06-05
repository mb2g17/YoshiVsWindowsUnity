using Assets.Classes;
using Assets.Classes.Files;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles file button
/// </summary>
public class FileScript : MonoBehaviour
{
    /// <summary>
    /// Sprites for each file number
    /// </summary>
    public Sprite File1Sprite, File2Sprite, File3Sprite, File4Sprite;

    /// <summary>
    /// Toggle components
    /// </summary>
    public GameObject DifficultyNormal, DifficultyHard, DifficultyNightmare, End, TrueEnd, NightmareEnd;

    /// <summary>
    /// Text to edit
    /// </summary>
    public TextMeshProUGUI Level;

    /// <summary>
    /// The file number
    /// </summary>
    [Range(1, 4)]
    public int FileNo;

    /// <summary>
    /// Game data
    /// </summary>
    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        // Selects sprite based on file number
        Sprite fileSprite = File1Sprite;
        switch (FileNo)
        {
            case 2:
                fileSprite = File2Sprite;
                break;
            case 3:
                fileSprite = File3Sprite;
                break;
            case 4:
                fileSprite = File4Sprite;
                break;
        }
        GetComponent<Image>().sprite = fileSprite;

        // Loads game data
        gameData = FileManager.LoadFile(FileNo);

        // If we're doing a playthrough
        if (gameData.AreWeDoingPlaythrough)
        {
            // Selects a difficulty
            DifficultyNormal.SetActive(false);
            DifficultyHard.SetActive(false);
            DifficultyNightmare.SetActive(false);
            switch (gameData.Difficulty)
            {
                case Difficulty.NORMAL:
                    DifficultyNormal.SetActive(true);
                    break;
                case Difficulty.HARD:
                    DifficultyHard.SetActive(true);
                    break;
                case Difficulty.NIGHTMARE:
                    DifficultyNightmare.SetActive(true);
                    break;
            }

            // Sets level text
            Level.text = CamelCaseSplitter.SplitCamelCase(gameData.Level);
        }
        else
        {
            // Hides playthrough components
            DifficultyNormal.SetActive(false);
            DifficultyHard.SetActive(false);
            DifficultyNightmare.SetActive(false);
            Level.gameObject.SetActive(false);
        }

        // Reset endings
        End.SetActive(false);
        TrueEnd.SetActive(false);
        NightmareEnd.SetActive(false);

        // Checks endings
        if (gameData.GotEnding)
            End.SetActive(true);
        if (gameData.GotTrueEnding)
            TrueEnd.SetActive(true);
        if (gameData.GotNightmareEnding)
            NightmareEnd.SetActive(true);
    }

    public void OnClick()
    {
        // Sets game data to game state
        GameState.Instance.SetGameData(FileNo, gameData);

        // Go to main menu
        SceneManager.LoadScene("MainMenu");
    }
}
