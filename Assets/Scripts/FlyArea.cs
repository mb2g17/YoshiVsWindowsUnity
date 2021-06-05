using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an area that Yoshi can fly through, like the 
/// rainbow stairs in MSHQ Factory
/// </summary>
public class FlyArea : MonoBehaviour
{
    /// <summary>
    /// Yoshi's animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Yoshi's rigidbody
    /// </summary>
    private Rigidbody2D rigidbody2d;

    /// <summary>
    /// State
    /// 0 - Yoshi is walking around
    /// 1 - Yoshi is flying around
    /// </summary>
    private int state = 0;

    /// <summary>
    /// Yoshi
    /// </summary>
    public GameObject Yoshi;

    // Start is called before the first frame update
    void Start()
    {
        animator = Yoshi.GetComponent<Animator>();
        rigidbody2d = Yoshi.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enforce rigidbody properties
        if (state == 1)
        {
            rigidbody2d.drag = 10;
            rigidbody2d.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Start flying
            animator.SetBool("Fly", true);
            rigidbody2d.drag = 10;
            rigidbody2d.gravityScale = 0;

            // Set state
            state = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If this is yoshi flying
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Stop flying
            animator.SetBool("Fly", false);
            rigidbody2d.drag = 0;
            rigidbody2d.gravityScale = 1;

            // Set state
            state = 0;
        }
    }
}
