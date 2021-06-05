using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPlatformScript : MonoBehaviour
{
    /// <summary>
    /// Platform children
    /// </summary>
    private List<GameObject> _platforms = new List<GameObject>();

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Clippit script
    /// </summary>
    private ClippitBattleClippitScript _clippit;

    /// <summary>
    /// Thunder sound clip
    /// </summary>
    public AudioClip Thunder;

    /// <summary>
    /// Turn off sound clip
    /// </summary>
    public AudioClip TurnOff;

    /// <summary>
    /// Yoshi game object
    /// </summary>
    public GameObject Yoshi;

    // Start is called before the first frame update
    void Start()
    {
        // Gets components
        _audioSource = GetComponent<AudioSource>();
        _clippit = FindObjectOfType<ClippitBattleClippitScript>();

        // Adds children
        foreach (Transform child in transform)
            _platforms.Add(child.gameObject);

        // Start coroutine
        StartCoroutine("SetOffPlatforms");
    }

    /// <summary>
    /// Coroutine that runs the loop that sets off the platforms
    /// </summary>
    /// <returns>Coroutine</returns>
    private IEnumerator SetOffPlatforms()
    {
        while (true)
        {
            // Gets the platform Yoshi is on
            int platformNo = GetYoshiPlatform();

            // Fires platform
            StartCoroutine("FirePlatform", platformNo);

            // If we're over half health, do another
            if (_clippit.Health <= 25)
                StartCoroutine("FirePlatform", GetAdjacentPlatform(platformNo));

            // Waits for a few seconds
            yield return new WaitForSeconds(6.5f);
        }
    }

    /// <summary>
    /// Returns the ID of an adjacent platform
    /// </summary>
    /// <param name="platform">The platform ID</param>
    /// <returns>An adjacent platform</returns>
    private int GetAdjacentPlatform(int platform)
    {
        // If we're at the far left, pick the next one to the right
        if (platform == 0)
            return 1;
        // If we're at the far right, pick the next one to the left
        else if (platform == 7)
            return 6;
        else
        {
            // Randomly pick a platform
            if (Random.Range(0, 2) == 0)
                return platform - 1;
            else
                return platform + 1;
        }
    }

    /// <summary>
    /// Corourine that fires off a platform
    /// </summary>
    /// <param name="platformNo">The platform ID to fire off</param>
    /// <returns>Coroutine</returns>
    private IEnumerator FirePlatform(int platformNo)
    {
        // Sets off platform
        GameObject platform = _platforms[platformNo];
        platform.GetComponent<Animator>().SetTrigger("Fire");

        // Plays turn off sound
        _audioSource.PlayOneShot(TurnOff);

        // Waits for 1.5 seconds
        yield return new WaitForSeconds(1.5f);

        // Plays thunder sound
        _audioSource.PlayOneShot(Thunder);
    }

    /// <summary>
    /// Gets the platform Yoshi is on
    /// </summary>
    /// <returns>The ID of the platform Yoshi is on</returns>
    private int GetYoshiPlatform()
    {
        float xVal = Yoshi.transform.position.x - transform.position.x;
        xVal += 3.8f;
        xVal *= (8f / 7.6f);
        int platformNo = Mathf.FloorToInt(xVal);
        return Mathf.Clamp(platformNo, 0, 7);
    }
}
