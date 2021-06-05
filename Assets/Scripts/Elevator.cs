using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If this object moves around with this script, Yoshi will move along with it if
/// he is standing on top of it
/// </summary>
public class Elevator : MonoBehaviour
{
    /// <summary>
    /// Memorised position
    /// </summary>
    private Vector2 Position;

    /// <summary>
    /// List of all yoshis
    /// </summary>
    private Yoshi[] _yoshis;

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        _yoshis = FindObjectsOfType<Yoshi>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If there is a new position
        if (!Position.Equals(transform.position))
        {
            // Gets new position
            Vector2 newPosition = transform.position;

            // Transforms all the Yoshis
            foreach (Yoshi yoshi in _yoshis)
                if (yoshi.GetComponent<MovingPlatformEnabler>().Platform == gameObject)
                    yoshi.GetComponent<Rigidbody2D>().position += (newPosition - Position) * Time.timeScale;

            // Update position
            Position = transform.position;
        }
    }
}
