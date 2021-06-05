using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles keys from Microsoft secret HQ
/// </summary>
public class MicrosoftSecretHQKey : MonoBehaviour
{
    /// <summary>
    /// The locked area
    /// </summary>
    public BoxCollider2D LockedArea;

    /// <summary>
    /// Clip that plays when we're collected
    /// </summary>
    public AudioClip CollectClip;

    /// <summary>
    /// Where to play clip from
    /// </summary>
    public AudioSource AudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer sr = LockedArea.gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

        AudioSource.PlayOneShot(CollectClip);
        LockedArea.enabled = false;
        Destroy(gameObject);
    }
}
