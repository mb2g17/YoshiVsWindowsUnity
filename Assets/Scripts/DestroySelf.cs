using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used in animated objects when they need to kill themselves
/// </summary>
public class DestroySelf : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
