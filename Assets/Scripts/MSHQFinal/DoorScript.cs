using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    /// <summary>
    /// If true, yoshi is touching the door
    /// </summary>
    private bool yoshiTouching = false;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Player yoshi
    /// </summary>
    public GameObject PlayerYoshi;

    /// <summary>
    /// BGM object
    /// </summary>
    public GameObject BGM;

    /// <summary>
    /// Audio clip when yoshi goes through door
    /// </summary>
    public AudioClip DoorOpenClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // If player is trying to go up and yoshi is touching the door
        if (yoshiTouching && Input.GetAxis("Vertical") > 0)
        {
            // Hide yoshi
            PlayerYoshi.SetActive(false);

            // Opens door
            GetComponent<Animator>().SetTrigger("Open");

            // Stops music
            BGM.GetComponent<AudioSource>().Stop();

            // Play door open clip
            audioSource.PlayOneShot(DoorOpenClip);

            // Waits before going to next scene
            StartCoroutine("NextRoom");
        }
    }

    private IEnumerator NextRoom()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("BillGatesBossPrelude");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it's yoshi
        if (collision.GetComponent<Yoshi>() != null)
            yoshiTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If it's yoshi
        if (collision.GetComponent<Yoshi>() != null)
            yoshiTouching = false;
    }
}
