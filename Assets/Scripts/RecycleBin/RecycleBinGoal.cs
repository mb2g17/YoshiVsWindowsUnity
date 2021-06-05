using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecycleBinGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's Yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Show message box
            MessageBoxScript mbs = FindObjectOfType<MessageBoxScript>();
            mbs.SetText("All files in the recycle bin will now be deleted.");
            mbs.SetTitle("Recycle Bin");
            mbs.SetButton("OK");
            mbs.SetInfo();
            mbs.SetSmallBox();

            mbs.ShowMessageBox();

            // Sets up close event
            mbs.OnMessageBoxClose = () =>
            {
                // Goes to next scene
                SceneManager.LoadScene("YoshiWorry2");
            };
        }
    }
}
