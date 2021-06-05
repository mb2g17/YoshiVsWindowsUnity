using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClippitBattleClippitScript : MonoBehaviour
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
    /// If true, we are mad
    /// </summary>
    private bool _isMad = false;

    /// <summary>
    /// Health, 50 = full and 0 = empty
    /// </summary>
    public int Health = 50;

    /// <summary>
    /// Health bar in UI
    /// </summary>
    public GameObject HealthBar;

    /// <summary>
    /// Fireball prefab
    /// </summary>
    public GameObject FireballPrefab;

    /// <summary>
    /// Explosion prefab
    /// </summary>
    public GameObject ExplosionPrefab;

    /// <summary>
    /// Hurt audio clip
    /// </summary>
    public AudioClip Hurt;

    /// <summary>
    /// Fireball audio clip
    /// </summary>
    public AudioClip Fireball;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is Yoshi's tongue
        if (collision.GetComponent<TongueScript>() != null)
        {
            // If we are not being hurt
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                // Plays hurt animation
                _animator.SetTrigger("Hurt");

                // Plays hurt sound
                _audioSource.PlayOneShot(Hurt);

                // Changes health
                Health--;

                // Updates it with UI
                HealthBar.GetComponent<Image>().fillAmount = ((float)Health / 50f);

                // Creates explosion
                GameObject explosion = Instantiate(ExplosionPrefab);
                explosion.transform.position = transform.position + new Vector3(0, -0.445f);

                // If we're dead
                if (Health <= 0)
                {
                    // Go to cutscene
                    SceneManager.LoadScene("ClippitBattleAfter");
                }

                // If we need to get mad AND we are not mad already
                if (Health <= 25 && !_isMad)
                {
                    // Set mad flag
                    _isMad = true;

                    // Set mag trigger on animator
                    _animator.SetTrigger("Angry");
                }
            }
        }
    }

    public void FireballShoot()
    {
        // Fires fireball
        _audioSource.PlayOneShot(Fireball);

        // Creates a fierball
        GameObject fireball = Instantiate(FireballPrefab);
        fireball.transform.position = transform.position;
    }
}
