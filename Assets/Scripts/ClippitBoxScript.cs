using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles clippit box behaviour
/// </summary>
public class ClippitBoxScript : MonoBehaviour
{
    /// <summary>
    /// Animator
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Sound to play when we are flashing
    /// </summary>
    public AudioClip LightningClip;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Yoshi is above us, flash
        if (collision.GetComponent<Yoshi>() != null)
        {
            _animator.SetBool("Flash", true);
            collision.GetComponent<AudioSource>().PlayOneShot(LightningClip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If Yoshi is no longer above us, stop flashing
        if (collision.GetComponent<Yoshi>() != null)
            _animator.SetBool("Flash", false);
    }
}
