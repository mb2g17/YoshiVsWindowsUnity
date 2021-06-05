using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows a message box to show up after killing all enemies
/// </summary>
public class EndingMessageBoxScript : MonoBehaviour
{
    /// <summary>
    /// The info box to show after killing all enemies
    /// </summary>
    public GameObject InfoBox;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EnemyTextScript>().OnAllEnemiesDead = () =>
        {
            // Puts the info box where Yoshi is
            InfoBox.transform.position = FindObjectOfType<Yoshi>().transform.position;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
