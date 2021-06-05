using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dispenses money from Bill Gates
/// </summary>
public class MoneyDispenser : MonoBehaviour
{
    /// <summary>
    /// The nuzzle to shoot from
    /// </summary>
    public Transform Nuzzle;

    /// <summary>
    /// Prefab for the money object
    /// </summary>
    public GameObject MoneyPrefab;

    /// <summary>
    /// Prefab for the money particle object
    /// </summary>
    public GameObject MoneyParticlePrefab;

    /// <summary>
    /// Money dispense sound
    /// </summary>
    public AudioClip KachingClip;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Shoots money
    /// </summary>
    /// <param name="money">The money prefab to use</param>
    private void _ShootMoney(GameObject money)
    {
        // Instantiates money
        GameObject newMoney = Instantiate(money);
        newMoney.transform.position = Nuzzle.position;
    }

    /// <summary>
    /// Shoots normal money
    /// </summary>
    public void ShootMoney()
    {
        // Plays sound
        audioSource.PlayOneShot(KachingClip);

        _ShootMoney(MoneyPrefab);
    }

    /// <summary>
    /// Shoots particle money
    /// </summary>
    public void ShootMoneyParticle()
    {
        _ShootMoney(MoneyParticlePrefab);
    }
}
