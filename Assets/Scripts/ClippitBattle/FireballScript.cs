using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fireball script for clippit boss
/// </summary>
public class FireballScript : MonoBehaviour
{
    /// <summary>
    /// Animator
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The vector to move across each frame
    /// </summary>
    private Vector3 _movementVector;

    /// <summary>
    /// Speed of the fireball
    /// </summary>
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movementVector = new Vector2(Random.Range(-2f, 2f), -6).normalized * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move with the vector
        transform.position += _movementVector * Time.deltaTime;

        // If we've gone down too far
        if (transform.position.y < -4)
        {
            // Kill self
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is Yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Makes this fireball explode
            _animator.SetTrigger("Explode");
        }
    }
}
