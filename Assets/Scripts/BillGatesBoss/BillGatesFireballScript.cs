using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fireball script for fireball in Bill Gates boss
/// </summary>
public class BillGatesFireballScript : MonoBehaviour
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
    /// Speed this fireball should go in
    /// </summary>
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movementVector = (FindObjectOfType<Yoshi>().transform.position - transform.position).normalized * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move with the vector
        transform.position += _movementVector * Time.timeScale * Time.deltaTime;

        // If we've gone down too far
        if (transform.position.x < -10  || transform.position.y < -5)
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
