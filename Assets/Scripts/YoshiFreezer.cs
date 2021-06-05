using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides two methods to freeze and unfreeze Yoshi.
/// Used in cutscenes.
/// </summary>
public class YoshiFreezer : MonoBehaviour
{
    /// <summary>
    /// Yoshi
    /// </summary>
    public GameObject Yoshi;

    /// <summary>
    /// Freezes Yoshi in place
    /// </summary>
    public void FreezeYoshi()
    {
        Yoshi.GetComponent<Jump>().Enabled = false;
        Yoshi.GetComponent<PlayerMovement>().Enabled = false;
        Yoshi.GetComponent<Yoshi>().Invincible = true;
        Yoshi.GetComponent<Yoshi>().EnableTongue = false;

    }

    /// <summary>
    /// Lets Yoshi move again
    /// </summary>
    public void FreeYoshi()
    {
        Yoshi.GetComponent<Jump>().Enabled = true;
        Yoshi.GetComponent<PlayerMovement>().Enabled = true;
        Yoshi.GetComponent<Yoshi>().Invincible = false;
        Yoshi.GetComponent<Yoshi>().EnableTongue = true;
    }
}
