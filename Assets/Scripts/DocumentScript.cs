using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behaviour of the document enemies
/// </summary>
public class DocumentScript : MonoBehaviour
{
    /// <summary>
    /// How fast the document goes up
    /// </summary>
    public float Speed = 2;

    /// <summary>
    /// How far high the document should go up before dying
    /// </summary>
    public float MaxHeight = 10;

    // Update is called once per frame
    void Update()
    {
        // Go up
        transform.position += new Vector3(0, Speed) * Time.deltaTime;

        // If we're past the max height
        if (transform.position.y > MaxHeight)
        {
            // Kill ourselves
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draws how far we will go up
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, MaxHeight));
    }
}
