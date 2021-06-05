using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If you attach this to a game object, it will sustain throughout scenes
/// </summary>
public class DontDestroyMe : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
