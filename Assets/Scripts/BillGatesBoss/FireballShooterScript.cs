using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooterScript : MonoBehaviour
{
    /// <summary>
    /// Fireball prefab
    /// </summary>
    public GameObject FireballPrefab;

    /// <summary>
    /// The nuzzle at which we will shoot from
    /// </summary>
    public Transform Nuzzle;

    /// <summary>
    /// Fireball clip
    /// </summary>
    public AudioClip FireballClip;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Shoots a fireball
    /// </summary>
    public void ShootFireball()
    {
        GameObject fireball = Instantiate(FireballPrefab);
        fireball.transform.position = Nuzzle.transform.position;
        audioSource.PlayOneShot(FireballClip);
    }
}
