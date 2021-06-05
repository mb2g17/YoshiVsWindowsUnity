using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the level
/// </summary>
public class LevelMover : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Speed, 0, 0) * Time.timeScale * Time.deltaTime;
    }
}
