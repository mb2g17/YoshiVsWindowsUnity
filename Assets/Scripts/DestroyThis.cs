using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used in animation to destroy the object using an animation event
/// </summary>
public class DestroyThis : MonoBehaviour
{
    /// <summary>
    /// Destroys this object
    /// </summary>
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
