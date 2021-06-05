using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the object to be moved all around
/// </summary>
public class AllAroundPlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Rigidbody 2d
    /// </summary>
    private Rigidbody2D _rigidbody2D;

    /// <summary>
    /// Direction we are facing in
    /// </summary>
    public Direction Direction = Direction.LEFT;

    /// <summary>
    /// How forceful the movement should be
    /// </summary>
    public int MovementForce = 10;

    /// <summary>
    /// Maximum speed we should go
    /// </summary>
    public int MaxSpeed = 20;

    /// <summary>
    /// If false, we can't move
    /// </summary>
    public bool Enabled = true;

    /// <summary>
    /// If true, the sprite will turn around
    /// </summary>
    public bool Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        StartCoroutine("Tick");
    }

    /// <summary>
    /// Sets the direction
    /// </summary>
    /// <param name="newDirection">The new direction to set</param>
    private void setDirection(Direction newDirection)
    {
        // If enabled, flips depending on direction
        if (Enabled && Turn)
        {
            this.transform.localScale = new Vector3(
                newDirection == Direction.LEFT ? -3 : 3,
                this.transform.localScale.y, 1);
        }

        // Sets direction
        this.Direction = newDirection;
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            // -- HORIZONTAL --
            // If we go left
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (Enabled)
                    _rigidbody2D.AddForce(new Vector2(-MovementForce * Time.timeScale, 0));
                setDirection(Direction.LEFT);
            }
            // If we go right
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (Enabled)
                    _rigidbody2D.AddForce(new Vector2(MovementForce * Time.timeScale, 0));
                setDirection(Direction.RIGHT);
            }

            // -- VERTICAL --
            // If we go up
            if (Input.GetAxis("Vertical") > 0)
            {
                if (Enabled)
                    _rigidbody2D.AddForce(new Vector2(0, MovementForce * Time.timeScale));
            }
            // If we go down
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (Enabled)
                    _rigidbody2D.AddForce(new Vector2(0, -MovementForce * Time.timeScale));
            }

            // If enabled...
            if (Enabled)
            {
                // Cap our speeds
                _rigidbody2D.velocity = new Vector2(
                    _rigidbody2D.velocity.x > MaxSpeed ? MaxSpeed : _rigidbody2D.velocity.x < -MaxSpeed ? -MaxSpeed : _rigidbody2D.velocity.x,
                    _rigidbody2D.velocity.y > MaxSpeed ? MaxSpeed : _rigidbody2D.velocity.y < -MaxSpeed ? -MaxSpeed : _rigidbody2D.velocity.y
                );
            }

            yield return new WaitForSeconds(1f / 60f);
        }
    }
}
