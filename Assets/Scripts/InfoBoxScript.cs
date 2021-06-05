using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles info box behaviour
/// </summary>
public class InfoBoxScript : MonoBehaviour
{
    /// <summary>
    /// What text to display
    /// </summary>
    [TextArea]
    public string Text;

    /// <summary>
    /// What should the title say
    /// </summary>
    public string Title;

    /// <summary>
    /// What should the button say
    /// </summary>
    public string Button;

    /// <summary>
    /// If true, use a big box
    /// </summary>
    public bool BigBox = false;

    public enum InfoIcon
    {
        WARNING, INFO, ERROR
    }
    /// <summary>
    /// What icon should the message box have
    /// </summary>
    public InfoIcon Icon = InfoIcon.INFO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we touch Yoshi
        if (collision.gameObject.GetComponent<Yoshi>() != null)
        {
            // Show message box
            MessageBoxScript mbs = FindObjectOfType<MessageBoxScript>();
            mbs.SetText(Text);
            mbs.SetTitle(Title);
            mbs.SetButton(Button);
            mbs.SetInfo();

            switch (Icon)
            {
                case InfoIcon.WARNING:
                    mbs.SetWarning();
                    break;
                case InfoIcon.INFO:
                    mbs.SetInfo();
                    break;
                case InfoIcon.ERROR:
                    mbs.SetError();
                    break;
            }

            if (BigBox)
                mbs.SetBigBox();
            else
                mbs.SetSmallBox();

            mbs.ShowMessageBox();

            // Destroy self
            Destroy(gameObject);
        }
    }
}
