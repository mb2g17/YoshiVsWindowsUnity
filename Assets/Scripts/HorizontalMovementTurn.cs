using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementTurn : MonoBehaviour
{
    /// <summary>
    /// Horizontal movement
    /// </summary>
    private HorizontalMovement _horizontalMovement;

    /// <summary>
    /// The scale we should set if we're facing left
    /// </summary>
    private float leftScale;

    /// <summary>
    /// The original direction we're facing in
    /// </summary>
    public Direction OriginalDirection = Direction.RIGHT;

    // Start is called before the first frame update
    void Start()
    {
        _horizontalMovement = GetComponent<HorizontalMovement>();

        leftScale = transform.localScale.x;
        if (OriginalDirection == Direction.RIGHT)
            leftScale *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're going left, turn left, else turn right
        if (_horizontalMovement.MovementState == HorizontalMovement.State.LEFT)
            transform.localScale = new Vector3(leftScale, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(leftScale * -1, transform.localScale.y, transform.localScale.z);
    }
}
