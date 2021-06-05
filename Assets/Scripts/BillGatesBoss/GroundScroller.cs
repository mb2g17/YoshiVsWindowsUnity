using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    /// <summary>
    /// The two gameobjects to scroll from
    /// </summary>
    public GameObject GroundLeft, GroundRight;

    public float Speed = 1;

    /// <summary>
    /// The position to reset grounds to
    /// </summary>
    private float resetX;

    // Start is called before the first frame update
    void Start()
    {
        resetX = GroundRight.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves ground
        GroundLeft.transform.Translate(-Speed * Time.timeScale * Time.deltaTime, 0, 0);
        GroundRight.transform.Translate(-Speed * Time.timeScale * Time.deltaTime, 0, 0);

        // If they've gone too far, move them back
        Array.ForEach(new GameObject[] { GroundLeft, GroundRight }, ground =>
        {
            if (ground.transform.position.x < -resetX)
            {
                ground.transform.Translate(2 * resetX, 0, 0);
            }
        });
    }
}
