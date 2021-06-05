using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles money in Bill Gates boss
/// </summary>
public class MoneyScriptDown : MonoBehaviour
{
    /// <summary>
    /// How fast money will move
    /// </summary>
    public float Speed = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -Speed * Time.timeScale * Time.deltaTime, 0);

        // If we've gone too far
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
}
