using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Size of the camera
    /// </summary>
    private float _height, _width;

    /// <summary>
    /// The object to follow
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// How far the camera can go, from origin
    /// </summary>
    public float MaxLeft = 10, MaxRight = 10, MaxTop = 5, MaxBottom = 5;

    /// <summary>
    /// If true, horizotal scrolling is frozen
    /// </summary>
    public bool FreezeHorizontal = false;

    /// <summary>
    /// If true, vertical scrolling is frozen
    /// </summary>
    public bool FreezeVertical = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get our width and height
        UpdateSizes();
    }

    private void UpdateSizes()
    {
        Camera cam = Camera.main;
        _height = 2f * cam.orthographicSize;
        _width = _height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the sizes
        UpdateSizes();

        // New position
        Vector3 newPos = new Vector3(0, 0);

        // If horizontal scrolling is not fixed
        if (!FreezeHorizontal)
        {
            // Sets new X
            if (Target.transform.position.x - (_width / 2f) < -MaxLeft)  // If X is too far left
                newPos += new Vector3(-MaxLeft + (_width / 2f), 0);
            else if (Target.transform.position.x + (_width / 2f) > MaxRight) // If X is too far right
                newPos += new Vector3(MaxRight - (_width / 2f), 0);
            else // If X is just right
                newPos += new Vector3(Target.transform.position.x, 0);
        }

        // If vertical scrolling is not fixed
        if (!FreezeVertical)
        {
            // Sets new Y
            if (Target.transform.position.y - (_height / 2f) < -MaxBottom)  // If Y is too far down
                newPos += new Vector3(0, -MaxBottom + (_height / 2f));
            else if (Target.transform.position.y + (_height / 2f) > MaxTop) // If Y is too far up
                newPos += new Vector3(0, MaxTop - (_height / 2f));
            else // If Y is just right
                newPos += new Vector3(0, Target.transform.position.y);
        }

        // Sets Z
        newPos += new Vector3(0, 0, -10);

        // Updates new position
        transform.position = newPos;
    }
    
    private void OnDrawGizmos()
    {
        // Draws a rectangle of the camera's limits
        Gizmos.color = Color.blue;

        // If vertical scrolling is not fixed
        if (!FreezeVertical)
        {
            Gizmos.DrawLine(new Vector3(-MaxLeft, MaxTop), new Vector3(MaxRight, MaxTop)); // Top line
            Gizmos.DrawLine(new Vector3(-MaxLeft, -MaxBottom), new Vector3(MaxRight, -MaxBottom)); // Bottom line
        }

        // If horizontal scrolling not fixed
        if (!FreezeHorizontal)
        {
            Gizmos.DrawLine(new Vector3(-MaxLeft, MaxTop), new Vector3(-MaxLeft, -MaxBottom)); // Left line
            Gizmos.DrawLine(new Vector3(MaxRight, MaxTop), new Vector3(MaxRight, -MaxBottom)); // Right line
        }
    }
}
