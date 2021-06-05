using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ensures the time is set on a text component
/// </summary>
[RequireComponent(typeof(TextMeshPro))]
public class TimeSetter : MonoBehaviour
{
    /// <summary>
    /// Text component
    /// </summary>
    private TextMeshPro _text;

    // Start is called before the first frame update
    void Start()
    {
        // Gets component
        _text = GetComponent<TextMeshPro>();

        // Updates with time
        StartCoroutine("SetTime");
    }

    private IEnumerator SetTime()
    {
        while (true)
        {
            _text.text = DateTime.Now.ToString("h:mm tt"); // Sets time
            yield return new WaitForSeconds(5); // Waits for 5 secs
        }
    }
}
