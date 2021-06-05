using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flamethrower script for Bill Gates
/// </summary>
public class FlamethrowerScript : MonoBehaviour
{
    /// <summary>
    /// Animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Yellow flash prefab
    /// </summary>
    public GameObject YellowFlashPrefab;

    /// <summary>
    /// Yellow Flash clip
    /// </summary>
    public AudioClip FlashClip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Shoots the yellow flash
    /// </summary>
    public void ShootYellowFlash()
    {
        // Spins flamethrower
        StartCoroutine("SpinFlamethrower");
    }

    private IEnumerator SpinFlamethrower()
    {
        // Spins
        animator.SetBool("Spin", true);

        // Waits for a couple of spins
        yield return new WaitForSeconds(1.5f);

        // Plays audio
        audioSource.PlayOneShot(FlashClip);

        // Creates prefab
        GameObject gameObject = Instantiate(YellowFlashPrefab);

        // Stops spinning
        animator.SetBool("Spin", false);
    }
}
