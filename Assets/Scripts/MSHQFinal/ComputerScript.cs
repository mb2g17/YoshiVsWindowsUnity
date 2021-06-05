using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScript : MonoBehaviour
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
    /// Yoshi
    /// </summary>
    private GameObject yoshi;

    /// <summary>
    /// Audio clip that plays when we're docile
    /// </summary>
    public AudioClip DocileClip;

    /// <summary>
    /// If true, we are always docile
    /// </summary>
    public bool AlwaysDocile = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        yoshi = FindObjectOfType<Yoshi>().gameObject;

        // If we are docile
        if (AlwaysDocile)
            animator.SetBool("Docile", true);
    }

    private void Update()
    {
        // Turn to face Yoshi
        transform.localScale = new Vector3(
            yoshi.transform.position.x < transform.position.x ? 3 : -3,
            3, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is a tongue AND "always docile" flag is false AND we're not docile already
        if (collision.GetComponent<TongueScript>() != null && !AlwaysDocile && !animator.GetBool("Docile"))
        {
            // Go docile and set flag
            animator.SetBool("Docile", true);
            StartCoroutine("GoEvil");

            // Play sound
            audioSource.PlayOneShot(DocileClip);
        }
    }

    private IEnumerator GoEvil()
    {
        yield return new WaitForSeconds(4);

        // If "always docile" flag is not set
        if (!AlwaysDocile)
            animator.SetBool("Docile", false);
    }
}
