using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes Yoshi turn every now and then
/// </summary>
public class YoshiTurn : MonoBehaviour
{
    /// <summary>
    /// Rate at which Yoshi will turn
    /// </summary>
    public float TurnRate = 5f;

    private void Start()
    {
        StartCoroutine("Turn");
    }

    private IEnumerator Turn()
    {
        while (true)
        {
            // Flips 
            if (Random.value < 1f / TurnRate)
                transform.localScale = new Vector3(transform.localScale.x * -1, 3, 1);

            yield return new WaitForSeconds(1f / 120f);
        }
    }
}
