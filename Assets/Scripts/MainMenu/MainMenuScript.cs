using Assets.Classes;
using Assets.Classes.Files;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// The image of the continue game button
    /// </summary>
    public Image ContinueGameButton;

    /// <summary>
    /// The image of the delete progress button
    /// </summary>
    public Image DeleteProgressButton;

    /// <summary>
    /// Text on the info panel of continue game button
    /// </summary>
    public TextMeshProUGUI ContinueGamePanelText;

    /// <summary>
    /// The preview yoshis to show what playthroughs you've beaten
    /// </summary>
    public GameObject NormalYoshi, HardYoshi, NightmareYoshi;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEverything();

        /*DEBUG
        FileManager.SaveFile(1, new GameData(true, true, true, true, 100, Difficulty.NIGHTMARE, "BillGatesBoss"));
        FileManager.SaveFile(2, new GameData(true, true, true, false, 100, Difficulty.HARD, "BillGatesBoss"));
        FileManager.SaveFile(3, new GameData(true, true, false, false, 100, Difficulty.NORMAL, "BillGatesBoss"));
        FileManager.SaveFile(4, new GameData(true, false, false, false, 100, Difficulty.NORMAL, "BillGatesBoss"));*/
    }

    /// <summary>
    /// Starts the game off
    /// </summary>
    /// <param name="difficulty">The difficulty to play at</param>
    public void StartGame(Difficulty difficulty)
    {
        // Sets up game state
        GameState.Instance.GameData.Difficulty = difficulty;
        GameState.Instance.GameData.Health = 100;
        GameState.Instance.GameData.Level = "Beginning";
        GameState.Instance.GameData.AreWeDoingPlaythrough = true;

        // Saves game
        GameState.Instance.SaveGameData();

        // Go to beginnings scene
        SceneManager.LoadScene("Beginning");
    }

    /// <summary>
    /// Calls all the update methods
    /// </summary>
    public void UpdateEverything()
    {
        UpdateContinueGameButton();
        UpdateContinueGamePanel();
        UpdateYoshis();
    }

    /// <summary>
    /// Updates the main menu buttons
    /// </summary>
    private void UpdateContinueGameButton()
    {
        // Sets colour of continue game button depending on state
        if (GameState.Instance.GameData.AreWeDoingPlaythrough)
        {
            ContinueGameButton.color = Color.white;
        }
        else {
            ContinueGameButton.color = new Color(0.168f, 0.168f, 0.168f);
        }
    }

    /// <summary>
    /// Updates continue game panel
    /// </summary>
    private void UpdateContinueGamePanel()
    {
        if (GameState.Instance.GameData.AreWeDoingPlaythrough)
        {
            ContinueGamePanelText.text =
                GameState.Instance.GameData.Difficulty.ToString() + "\n" +
                CamelCaseSplitter.SplitCamelCase(GameState.Instance.GameData.Level);
        }
        else
            ContinueGamePanelText.text = "Start a new game first before continuing one!";
    }

    /// <summary>
    /// Updates the preview yoshis
    /// </summary>
    private void UpdateYoshis()
    {
        NormalYoshi.SetActive(false);
        HardYoshi.SetActive(false);
        NightmareYoshi.SetActive(false);

        if (GameState.Instance.GameData.GotEnding)
            NormalYoshi.SetActive(true);
        if (GameState.Instance.GameData.GotTrueEnding)
            HardYoshi.SetActive(true);
        if (GameState.Instance.GameData.GotNightmareEnding)
            NightmareYoshi.SetActive(true);
    }

    /// <summary>
    /// Continues the game where we left off
    /// </summary>
    public void ContinueGame()
    {
        // Only continue if we have a game to play
        if (GameState.Instance.GameData.AreWeDoingPlaythrough)
            SceneManager.LoadScene(GameState.Instance.GameData.Level);
    }

    /// <summary>
    /// Deletes progress
    /// </summary>
    public void DeleteGame()
    {
        // Wipes file
        GameState.Instance.SetGameData(GameState.Instance.FileNo,
            new Assets.Classes.Files.GameData(false, false, false, false, 100, Assets.Classes.Difficulty.NORMAL, "MushroomKingdom"));
        GameState.Instance.SaveGameData();

        // Refreshes buttons and panels
        UpdateEverything();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
