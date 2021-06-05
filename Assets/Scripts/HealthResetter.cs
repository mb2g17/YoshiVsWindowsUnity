using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Resets health
        GameState.Instance.Health = 100;

        // Destroys self
        Destroy(gameObject);
    }
}
