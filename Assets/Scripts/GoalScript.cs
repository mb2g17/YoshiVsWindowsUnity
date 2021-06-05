using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    /// <summary>
    /// The next scene to go to when this goal is touched
    /// </summary>
    public string NextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we hit Yoshi, go to next scene
        if (collision.GetComponent<Yoshi>() != null)
            SceneManager.LoadScene(NextScene);
    }
}
