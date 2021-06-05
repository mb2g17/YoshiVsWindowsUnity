using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the fireball shooter point at Yoshi
/// </summary>
public class PointAtYoshi : MonoBehaviour
{
    /// <summary>
    /// Yoshi
    /// </summary>
    public GameObject YoshiObj;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = YoshiObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
