using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class InternetExplorationGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's Yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Show message box
            MessageBoxScript mbs = FindObjectOfType<MessageBoxScript>();
            mbs.SetText("Internet Explorer has caused an illegal operation for no reason.");
            mbs.SetTitle("Microsoft Internet Explorer");
            mbs.SetButton("OK");
            mbs.SetError();
            mbs.SetSmallBox();

            mbs.ShowMessageBox();

            // Sets up close event
            mbs.OnMessageBoxClose = () =>
            {
                // Goes to next scene
                SceneManager.LoadScene("YoshiError");
            };
        }
    }
}
