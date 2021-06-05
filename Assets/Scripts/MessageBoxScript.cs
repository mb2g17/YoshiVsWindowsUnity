using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles message box UI
/// </summary>
public class MessageBoxScript : MonoBehaviour
{
    private TapHandler _submitHandler;

    public GameObject WarningIcon;
    public GameObject InfoIcon;
    public GameObject ErrorIcon;
    public GameObject Text;
    public GameObject Title;
    public GameObject Button;
    public GameObject Box;

    /// <summary>
    /// Event that is fired when message box is closed
    /// </summary>
    public Action OnMessageBoxClose = () => { };

    private void Start()
    {
        // Sets up submit handler
        _submitHandler = new TapHandler(() => Input.GetAxisRaw("Submit") > 0);
    }

    private void Update()
    {
        // Updates submit handler
        _submitHandler.Update();

        // If we're pressing submit
        if (_submitHandler.IsTapped())
        {
            // We've effectively pressed OK
            OKPress();
        }
    }

    public void SetText(string newText)
    {
        Text.GetComponent<Text>().text = newText;
    }

    public void SetTitle(string newTitle)
    {
        Title.GetComponent<Text>().text = newTitle;
    }

    public void SetButton(string newButtonText)
    {
        Button.GetComponent<Text>().text = newButtonText;
    }

    public void SetWarning()
    {
        InfoIcon.SetActive(false);
        WarningIcon.SetActive(true);
        ErrorIcon.SetActive(false);
    }

    public void SetInfo()
    {
        InfoIcon.SetActive(true);
        WarningIcon.SetActive(false);
        ErrorIcon.SetActive(false);
    }

    public void SetError()
    {
        InfoIcon.SetActive(false);
        WarningIcon.SetActive(false);
        ErrorIcon.SetActive(true);
    }

    public void SetBigBox()
    {
        Box.GetComponent<RectTransform>().sizeDelta = new Vector2(561.5f, Box.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void SetSmallBox()
    {
        Box.GetComponent<RectTransform>().sizeDelta = new Vector2(311.5f, Box.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void ShowMessageBox()
    {
        Box.SetActive(true); // Shows box
        Time.timeScale = 0; // Stops time
    }

    /// <summary>
    /// When the user presses the button
    /// </summary>
    public void OKPress()
    {
        // If the box is active
        if (Box.activeSelf)
        {
            Box.SetActive(false); // Hides box
            Time.timeScale = 1; // Starts time
            OnMessageBoxClose(); // Run event

            StartCoroutine("TemporarilyDisableTongue");
        }
    }

    private IEnumerator TemporarilyDisableTongue()
    {
        // Temporarily disables yoshi's tongue
        Yoshi yoshi = FindObjectOfType<Yoshi>();
        yoshi.EnableTongue = false;
        yield return new WaitForSeconds(0.1f);
        yoshi.EnableTongue = true;
    }
}
