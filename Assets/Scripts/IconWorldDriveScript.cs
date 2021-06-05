using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles movement of Icon World drive
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Elevator))]
public class IconWorldDriveScript : MonoBehaviour
{
    /// <summary>
    /// True if we're waiting for Yoshi
    /// </summary>
    private bool _waitingForYoshi = true;

    /// <summary>
    /// Box collider 2d
    /// </summary>
    private BoxCollider2D _boxCollider2D;

    /// <summary>
    /// Horizontal movement
    /// </summary>
    private HorizontalMovement _horizontalMovement;

    /// <summary>
    /// Rigidbody 2d
    /// </summary>
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _horizontalMovement = GetComponent<HorizontalMovement>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // Disables horizontal movement and rigidbody for now
        _horizontalMovement.enabled = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;

        // When fully left, fall
        _horizontalMovement.TurnRightEvent = () =>
        {
            _horizontalMovement.enabled = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Elevator>().enabled = false;
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it's Yoshi AND we're waiting for him AND he's on top
        if (collision.gameObject.GetComponent<Yoshi>() != null &&
            _waitingForYoshi &&
            collision.transform.position.y > transform.position.y &&
            collision.transform.position.x < transform.position.x + 0.5f)
        {
            // We're no longer waiting for him
            _waitingForYoshi = false;

            // Move horizontally
            _horizontalMovement.enabled = true;
        }
    }
}
