using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoshi : MonoBehaviour
{
    /** Animator that animates Yoshi */
    private Animator _animator;

    /** Rigidbody for Yoshi */
    private Rigidbody2D _rigidbody2d;

    /** The sprite renderer of Yoshi */
    private SpriteRenderer _spriteRenderer;

    /** The audio source */
    private AudioSource _audioSource;

    /** Horizontal movement script */
    private PlayerMovement _playerMovement;

    /** Jump script */
    private Jump _jump;

    /** The tongue object */
    private GameObject _tongue;

    /// <summary>
    /// Yoshi's original position
    /// </summary>
    private Vector3 _originalPos;

    /** Audio clip */
    public AudioClip JumpClip;
    public AudioClip TongueClip;
    public AudioClip WarpClip;
    public AudioClip EatClip;

    /// <summary>
    /// If true, nothing can harm Yoshi
    /// </summary>
    public bool Invincible = false;

    /// <summary>
    /// If true, we can use our tongue
    /// </summary>
    public bool EnableTongue = true;

    /// <summary>
    /// Event that is run when Yoshi pulls his tongue
    /// </summary>
    public Action OnTonguePull = () => { };

    // Start is called before the first frame update
    void Start()
    {
        // Gets components
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _jump = GetComponent<Jump>();
        _audioSource = GetComponent<AudioSource>();

        // Sets original position
        _originalPos = transform.position;

        // Gets children
        _tongue = transform.GetChild(0).gameObject;

        // Sets up horizontal movement events
        _playerMovement.MoveEvent = () => _animator.SetBool("Run", true);
        _playerMovement.StopEvent = () => _animator.SetBool("Run", false);

        // Sets up jump scripts
        _jump.JumpEvent = () =>
        {
            _animator.SetBool("Fall", false);
            _animator.SetBool("Land", false);
            _animator.SetBool("Jump", true);
            _audioSource.PlayOneShot(JumpClip); // Plays jump sound effect
        };
        _jump.FallEvent = () =>
        {
            _animator.SetBool("Fall", true);
            _animator.SetBool("Land", false);
            _animator.SetBool("Jump", false);
        };
        _jump.LandEvent = () =>
        {
            _animator.SetBool("Fall", false);
            _animator.SetBool("Land", true);
            _animator.SetBool("Jump", false);
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Set eat clip flag back
        _playEatClip = true;

        // If we are using our tongue AND we're not doing it already AND time hasn't stopped AND tongue is enabled
        if (Input.GetButtonDown("Tongue") && !_animator.GetBool("Tongue") && Time.timeScale > 0 && EnableTongue)
        {
            // Animates tongue and plays sound
            _animator.SetBool("Tongue", true);
            _audioSource.PlayOneShot(TongueClip);

            // Runs event
            OnTonguePull();
        }
    }

    /// <summary>
    /// Stops the animation from using tongue
    /// </summary>
    public void StopTongue()
    {
        _animator.SetBool("Tongue", false);
    }

    /// <summary>
    /// Plays the eat sound clip
    /// </summary>
    private bool _playEatClip = true; // Flag so enemies don't stack sfx
    public void PlayEatClip()
    {
        // If we can play, play it
        if (_playEatClip)
        {
            _audioSource.PlayOneShot(EatClip);
            _playEatClip = false;
        }
    }

    /// <summary>
    /// Kills enemies on our tongue
    /// </summary>
    public void KillEnemies()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("KillMe"))
        {
            Destroy(obj);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we're touching the portal
        if (collision.gameObject.CompareTag("Portal"))
        {
            // Go back to original position
            transform.position = _originalPos;

            // Plays warp sound
            _audioSource.PlayOneShot(WarpClip);
        }
    }
}
