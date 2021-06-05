using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFriendTriggerScript : MonoBehaviour
{
    /// <summary>
    /// Prefab of cursor player
    /// </summary>
    public GameObject CursorFriendPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Yoshi touched us
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Destroy normal Yoshi
            Destroy(collision.gameObject);

            // Create cursor yoshi
            GameObject cursorYoshi = Instantiate(CursorFriendPrefab);
            cursorYoshi.transform.position = transform.position;

            // Set camera to focus on cursor yoshi
            Camera.main.GetComponent<CameraFollow>().Target = cursorYoshi;

            // Destroy us
            Destroy(gameObject);
        }
    }
}
