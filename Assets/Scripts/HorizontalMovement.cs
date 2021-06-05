using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Horizontally moves an object back and forth
/// </summary>
public class HorizontalMovement : MonoBehaviour
{
    public enum State
    {
        RANDOM, LEFT, RIGHT
    }
    /// <summary>
    /// Direction
    /// </summary>
    public State MovementState = State.RANDOM;

    /// <summary>
    /// The position we were at, at the start
    /// </summary>
    private Vector3 _originalPosition;

    /// <summary>
    /// Stores if we are moving
    /// </summary>
    private bool _moving;

    /// <summary>
    /// The corners at which this enemy will walk
    /// </summary>
    public float LeftCorner = 1, RightCorner = 1;

    /// <summary>
    /// Speed of the enemy
    /// </summary>
    public float Speed = 0.03f;

    /// <summary>
    /// Event that is called when turning right
    /// </summary>
    public Action TurnRightEvent = () => { };

    /// <summary>
    /// Event that is called when turning left
    /// </summary>
    public Action TurnLeftEvent = () => { };

    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position; // Remembers original position
        if (MovementState == State.RANDOM)
            MovementState = UnityEngine.Random.Range(0, 2) == 0 ? State.LEFT : State.RIGHT; // Goes in a random direction
        _moving = true; // We are moving
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If we're going left
        if (MovementState == State.LEFT)
        {
            // Move platform
            transform.position += new Vector3(-Speed, 0) * Time.timeScale;

            // If we've reached the end
            if (transform.position.x < _originalPosition.x - LeftCorner)
            {
                MovementState = State.RIGHT; // Go the other way
                TurnRightEvent(); // Run event
            }
        }

        // If we're going right
        if (MovementState == State.RIGHT)
        {
            // Move platform
            transform.position += new Vector3(Speed, 0) * Time.timeScale;

            // If we've reached the end
            if (transform.position.x > _originalPosition.x + RightCorner)
            {
                MovementState = State.LEFT; // Go the other way
                TurnLeftEvent(); // Run event
            }
        }
    }
    private void OnDrawGizmos()
    {
        // The vector we'll draw the line from
        Vector3 pivotVector = _moving ? _originalPosition : transform.position;

        // Draws the line the enemies will walk in
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            pivotVector - new Vector3(LeftCorner, 0),
            pivotVector + new Vector3(RightCorner, 0)
        );
    }
}
