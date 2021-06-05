using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The object can jump up and down
/// </summary>
public class Jump : MonoBehaviour
{
    /// <summary>
    /// The Rigidbody2d
    /// </summary>
    private Rigidbody2D _rigidbody2d;

    /// <summary>
    /// Our state (0 for not jumping, 1 for jumping, 2 for falling)
    /// </summary>
    private int _state = 0;

    /// <summary>
    /// How high we should jump
    /// </summary>
    public float Height = 5;

    /// <summary>
    /// The maximum speed at which we should fall
    /// </summary>
    public float MaxFallSpeed = 10;

    /// <summary>
    /// The types of gravity to switch between
    /// </summary>
    public float LongJumpGravity = 0.5f, SmallJumpGravity = 1.5f, FallGravity = 2.5f;

    /// <summary>
    /// Events that fire off under certain conditions
    /// </summary>
    public Action JumpEvent = () => { },
                FallEvent = () => { },
                LandEvent = () => { };

    /// <summary>
    /// If false, we can't jump
    /// </summary>
    public bool Enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        // Gets components
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _rigidbody2d.gravityScale = 2.5f; // Set this as default fall strength
    }

    // Update is called once per frame
    void Update()
    {
        // If we're not jumping
        if (_state == 0)
        {
            // If we're pressing up AND we're enabled
            if (Input.GetAxis("Jump") > 0 && Enabled)
            {
                _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, Height); // Jump
                _state = 1; // Change state to jumping
                JumpEvent(); // Execute the jump event
            }

            // If we're already falling down fast enough
            if (_rigidbody2d.velocity.y < -0.5f)
            {
                _state = 2; // Change state to falling
                FallEvent(); // Execute the fall event
            }
        }
        
        // If we've jumped
        if (_state == 1)
        {
            // If we're holding up AND we're enabled
            if (Input.GetAxis("Jump") > 0 && Enabled)
            {
                _rigidbody2d.gravityScale = LongJumpGravity; // Accelerate up (do a longer jump)
            }
            else
            {
                _rigidbody2d.gravityScale = SmallJumpGravity; // Do a small jump
            }

            // If we're going down now
            if (_rigidbody2d.velocity.y <= 0)
            {
                _state = 2; // Change state to falling
                FallEvent(); // Execute the fall event
                _rigidbody2d.gravityScale = FallGravity; // Make Yoshi fall faster
                _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, -0.5f); // Starts Yoshi's fall (stops us from landing as soon as we've started falling)
            }
        }

        // If we're falling
        if (_state == 2)
        {
            // If we're falling too fast
            if (_rigidbody2d.velocity.y < -MaxFallSpeed)
            {
                _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, -MaxFallSpeed);
            }

            // If we're suddenly no longer falling
            if (_rigidbody2d.velocity.y >= -0.25f)
            {
                _state = 0; // Go back to normal
                LandEvent(); // Execute the land event
            }
        }
    }
}
