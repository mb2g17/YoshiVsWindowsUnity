using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippitFloorScript : MonoBehaviour
{
    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// The component responsible for Yoshi's health text GUI
    /// </summary>
    private HealthTextScript _healthTextScript;

    /// <summary>
    /// Clip that plays when we're set back
    /// </summary>
    public AudioClip SetBackClip;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _healthTextScript = FindObjectOfType<HealthTextScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Set Yoshi back
            collision.transform.position = new Vector3(
                collision.transform.position.x,
                4.87f);

            // Deals 10 damage
            _healthTextScript.DeductHealth(10);

            // Play sound
            _audioSource.PlayOneShot(SetBackClip);
        }
    }
}
