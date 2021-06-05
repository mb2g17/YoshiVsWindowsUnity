using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loops a background
/// </summary>
public class LoopingBackground : MonoBehaviour
{
    /// <summary>
    /// Speed
    /// </summary>
    public float Speed = 0.2f;

    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * Speed, 0);
        offset.x = offset.x - Mathf.FloorToInt(offset.x);
        renderer.material.mainTextureOffset = offset;
    }
}
