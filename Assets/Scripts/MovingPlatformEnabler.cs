using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow Yoshi to remember what platform it's sitting on
/// </summary>
public class MovingPlatformEnabler : MonoBehaviour
{
    /// <summary>
    /// True if we are on a platform
    /// </summary>
    private bool _onPlatform = false;

    /// <summary>
    /// The platform we're standing on
    /// </summary>
    public GameObject Platform = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If this isn't a question block AND a block that can't be a moving platform
        if (!collision.gameObject.CompareTag("QuestionBlock") &&
            !collision.gameObject.CompareTag("UnMovingPlatformAble"))
        {
            //transform.parent = collision.transform;
            Platform = collision.gameObject;
            _onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_onPlatform)
        {
            //transform.parent = null;
            Platform = null;
            _onPlatform = false;
        }
    }
}
