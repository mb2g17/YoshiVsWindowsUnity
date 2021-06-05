using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BSODScript : MonoBehaviour
{
    /// <summary>
    /// BGM object
    /// </summary>
    public GameObject BGM;

    // Start is called before the first frame update
    void Start()
    {
        // Sets health back to 100
        GameState.Instance.Health = 100;
        GameState.Instance.OnHealthChange = (oldHealth, newHealth) => { };

        // Save
        GameState.Instance.SaveGameData();

        // If there are no "Don't Destroy Me" music playing, then play game over
        bool thereExists = false;
        foreach (AudioSource obj in FindObjectsOfType<AudioSource>())
        {
            if (obj.gameObject.GetComponent<DontDestroyMe>() != null)
                thereExists = true;
        }
        if (!thereExists)
            BGM.SetActive(true);
    }

    /// <summary>
    /// Continues the game where we left off
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene(GameState.Instance.GameData.Level);
    }

    public void ReturnToMenu()
    {
        // Destroy all Don't Destroy Me stuff
        foreach (DontDestroyMe obj in FindObjectsOfType<DontDestroyMe>())
            Destroy(obj.gameObject);

        // Goes to splash screen
        SceneManager.LoadScene("SplashScreen");
    }
}
