using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles common enemy behaviour
/// </summary>
[RequireComponent(typeof(Edible))]
[RequireComponent(typeof(Harmful))]
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The component responsible for the enemy text GUI
    /// </summary>
    private EnemyTextScript _enemyTextScript;

    /// <summary>
    /// The component that makes this enemy edible
    /// </summary>
    private Edible _edible;

    /// <summary>
    /// The component that makes this enemy harmful to Yoshi
    /// </summary>
    private Harmful _harmful;

    // Start is called before the first frame update
    void Start()
    {
        // Gets number of enemies text script
        _enemyTextScript = FindObjectOfType<EnemyTextScript>();

        // Gets components
        _edible = GetComponent<Edible>();
        _harmful = GetComponent<Harmful>();

        // Sets up events
        _edible.CaughtEvent = () =>
        {
            // Destroy horizontal movement
            Destroy(GetComponent<HorizontalMovement>());

            // Stops us from being harmful
            _harmful.enabled = false;

            // Deduct enemy count
            if (_enemyTextScript != null)
                _enemyTextScript.DeductCount();

            // Disables exploder, if we have one
            Exploder exploder = GetComponent<Exploder>();
            if (exploder != null)
                exploder.enabled = false;
        };
        _edible.EatenEvent = () => { };
    }
}
