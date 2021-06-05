using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes object lock on to another object's location
/// </summary>
public class LockOn : MonoBehaviour
{
    /// <summary>
    /// The object to lock on to
    /// </summary>
    public GameObject Target;

    // Update is called once per frame
    void Update()
    {
        // Moves to target location
        transform.position = new Vector3(
            Target.transform.position.x,
            Target.transform.position.y,
            transform.position.z
        );
    }
}
