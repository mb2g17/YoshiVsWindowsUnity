using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vertically moves an object back and forth
/// </summary>
public class VerticalMovement : MonoBehaviour
{
    public enum State
    {
        RANDOM, UP, DOWN
    }

    /// <summary>
    /// Direction
    /// </summary>
    public State MovementState = State.RANDOM;

    /** The position we were at, at the start */
    private Vector3 _originalPosition;

    /** Stores if we are moving */
    private bool _moving;

    /** The corners at which this enemy will walk */
    public float TopCorner = 1, BottomCorner = 1;

    /** Speed of the enemy */
    public float Speed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.position; // Remembers original position
        if (MovementState == State.RANDOM)
            MovementState = UnityEngine.Random.Range(0, 2) == 0 ? State.UP : State.DOWN; // Goes in a random direction
        _moving = true; // We are moving
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If we're going up
        if (MovementState == State.UP)
        {
            transform.position += new Vector3(0, Speed) * Time.timeScale;

            // If we've reached the end
            if (transform.position.y > _originalPosition.y + TopCorner)
            {
                MovementState = State.DOWN; // Go the other way
            }
        }

        // If we're going down
        if (MovementState == State.DOWN)
        {
            transform.position += new Vector3(0, -Speed) * Time.timeScale;

            // If we've reached the end
            if (transform.position.y < _originalPosition.y - BottomCorner)
            {
                MovementState = State.UP; // Go the other way
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
            pivotVector + new Vector3(0, TopCorner),
            pivotVector - new Vector3(0, BottomCorner)
        );
    }
}
