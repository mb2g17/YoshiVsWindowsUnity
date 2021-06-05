using Assets.Classes;
using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows pausing
/// </summary>
public class PausePanelScript : MonoBehaviour
{
    /// <summary>
    /// The pause panel animator
    /// </summary>
    public Animator PausePanelAnimator;

    /// <summary>
    /// Message box
    /// </summary>
    public GameObject MessageBox;

    private TapHandler tapHandler = new TapHandler(() => Input.GetAxisRaw("Pause") > 0);

    private void Start()
    {
        // Make sure it's not visible
        PausePanelAnimator.SetBool("Pause", false);
    }

    // Update is called once per frame
    void Update()
    {
        tapHandler.Update();

        if (tapHandler.IsTapped())
        {
            // If nobody is speaking right now AND message box isn't up
            if (FindObjectsOfType<SayDialog>().Length == 0 && !MessageBox.activeSelf)
            {
                // If we are paused
                if (PausePanelAnimator.GetBool("Pause"))
                {
                    // Unpause
                    Time.timeScale = 1;
                    PausePanelAnimator.SetBool("Pause", false);
                    Array.ForEach(FindObjectsOfType<AudioSource>(), (audio) => audio.volume = 1);
                }
                else
                {
                    // Pause
                    Time.timeScale = 0;
                    PausePanelAnimator.SetBool("Pause", true);
                    Array.ForEach(FindObjectsOfType<AudioSource>(), (audio) => audio.volume = 0.2f);
                }
            }
        }
    }

    public void OnBackToMenuPress()
    {
        // Restore all music volumes
        Array.ForEach(FindObjectsOfType<AudioSource>(), (audio) => audio.volume = 1);

        // Destroy all don't destroy me things (because it's usually music)
        Array.ForEach(FindObjectsOfType<DontDestroyMe>(), (ddm) => Destroy(ddm.gameObject));


        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnReturnPress()
    {
        Time.timeScale = 1;
        PausePanelAnimator.SetBool("Pause", false);
        Array.ForEach(FindObjectsOfType<AudioSource>(), (audio) => audio.volume = 1);
    }
}
