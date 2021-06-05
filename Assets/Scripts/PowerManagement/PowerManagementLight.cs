using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManagementLight : MonoBehaviour
{
    /// <summary>
    /// Animator
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Sound to play when turned on
    /// </summary>
    public AudioClip TurnOnClip;

    /// <summary>
    /// Sound to play when turned off
    /// </summary>
    public AudioClip TurnOffClip;

    /// <summary>
    /// The black covering everything
    /// </summary>
    public GameObject Black;

    /// <summary>
    /// If false, do not turn on when touched
    /// </summary>
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Animates
        _animator.SetBool("On", !Black.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we're not on AND we're touched by Yoshi AND we're enabled
        if (Black.activeSelf && collision.GetComponent<Yoshi>() != null && Enabled)
        {
            // Disables black
            Black.SetActive(false);

            // Plays turn on clip
            _audioSource.PlayOneShot(TurnOnClip);

            // After a while, make it active again
            StartCoroutine("TurnBlackOn");
        }
    }

    private IEnumerator TurnBlackOn()
    {
        yield return new WaitForSeconds(10);

        // Turn black back on
        Black.SetActive(true);

        // Plays off clip
        _audioSource.PlayOneShot(TurnOffClip);
    }
}
