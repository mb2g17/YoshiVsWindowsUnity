using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The object can move left and right
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Stores state (0 for idle, 1 for moving)
    /// </summary>
    private int _state = 0;

    /// <summary>
    /// The sprite renderer
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The Rigidbody2d
    /// </summary>
    private Rigidbody2D _rigidbody2d;

    /// <summary>
    /// Direction we are facing in
    /// </summary>
    public Direction Direction = Direction.RIGHT;

    /// <summary>
    /// The maximum speed at which we can move
    /// </summary>
    public float Speed;

    /// <summary>
    /// Events that fire off under certain conditions
    /// </summary>
    public Action MoveEvent = () => { },
                StopEvent = () => { };

    /// <summary>
    /// If false, we can't move
    /// </summary>
    public bool Enabled = true;

    public bool Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        // Gets components
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Sets the direction
    /// </summary>
    /// <param name="newDirection">The new direction to set</param>
    public void SetDirection(Direction newDirection)
    {
        // Flips depending on direction and if we should turn
        if (Turn)
            this.transform.localScale = new Vector3(
                newDirection == Direction.LEFT ? -3 : 3,
                this.transform.localScale.y, 1);

        // Sets direction
        this.Direction = newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're moving too fast, clamp it
        if (_rigidbody2d.velocity.x > Speed)
            _rigidbody2d.velocity = new Vector2(Speed, _rigidbody2d.velocity.y);
        if (_rigidbody2d.velocity.x < -Speed)
            _rigidbody2d.velocity = new Vector2(-Speed, _rigidbody2d.velocity.y);

        // If we are idle
        if (_state == 0)
        {
            // If we're moving in the wrong direction
            if (Direction == Direction.LEFT && _rigidbody2d.velocity.x > 0.5f ||
                Direction == Direction.RIGHT && _rigidbody2d.velocity.x < -0.5f)
            {
                // Stop
                _rigidbody2d.velocity = new Vector2(0, _rigidbody2d.velocity.y);
            }

            // If we're moving with some speed
            if (Math.Abs(_rigidbody2d.velocity.x) > 1f)
            {
                // Stop, depending on direction
                if (this.Direction == 0)
                    _rigidbody2d.velocity += new Vector2(0.5f, 0) * Time.deltaTime * 60;
                else
                    _rigidbody2d.velocity -= new Vector2(0.5f, 0) * Time.deltaTime * 60;
            }

            // If we've just about stopped, then stop
            if (Math.Abs(_rigidbody2d.velocity.x) <= 1f)
            {
                _rigidbody2d.velocity = new Vector2(0, _rigidbody2d.velocity.y);
            }

            // If we go left AND we're enabled
            if (Input.GetAxis("Horizontal") < 0 && Enabled)
            {
                this.SetDirection(Direction.LEFT);
                _state = 1;
                this.MoveEvent();
            }

            // If we go right AND we're enabled
            if (Input.GetAxis("Horizontal") > 0 && Enabled)
            {
                this.SetDirection(Direction.RIGHT);
                _state = 1;
                this.MoveEvent();
            }
        }

        // If we are running
        if (_state == 1)
        {
            // Move, depending on direction
            if (this.Direction == 0)
                _rigidbody2d.velocity += new Vector2(-1, 0) * Time.deltaTime * 60;
            else
                _rigidbody2d.velocity += new Vector2(1, 0) * Time.deltaTime * 60;

            // If we aren't moving OR if we are disabled, go back to idle state
            if (Input.GetAxis("Horizontal") == 0 || !Enabled)
            {
                _state = 0; // Change state
                this.StopEvent(); // Run stop event
            }

            // If we go left AND we're enabled
            if (Input.GetAxis("Horizontal") < 0 && Enabled)
            {
                this.SetDirection(Direction.LEFT);
            }

            // If we go right AND we're enabled
            if (Input.GetAxis("Horizontal") > 0 && Enabled)
            {
                this.SetDirection(Direction.RIGHT);
            }
        }
    }
}
