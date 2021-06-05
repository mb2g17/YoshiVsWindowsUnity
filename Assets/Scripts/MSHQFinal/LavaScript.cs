using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    /// <summary>
    /// List of waypoints
    /// </summary>
    public List<Vector2> Waypoints;

    /// <summary>
    /// Yoshi player
    /// </summary>
    public GameObject PlayerYoshi;

    /// <summary>
    /// Prefab of burning yoshi
    /// </summary>
    public GameObject YoshiBurnPrefab;

    /// <summary>
    /// Yoshi hurt clip
    /// </summary>
    public AudioClip YoshiHurtClip;

    /// <summary>
    /// Thunder clip
    /// </summary>
    public AudioClip ThunderClip;

    /// <summary>
    /// Mama mia clip
    /// </summary>
    public AudioClip YoshiMamaMiaClip;

    /// <summary>
    /// Audio source
    /// </summary>
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is yoshi
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Disable player yoshi
            PlayerYoshi.SetActive(false);

            // Harm Yoshi
            FindObjectOfType<HealthTextScript>().DeductHealth(10);

            // Create burning yoshi
            GameObject burningYoshi = Instantiate(YoshiBurnPrefab);
            burningYoshi.transform.position = PlayerYoshi.transform.position;

            // Play yoshi hurt clip and thunder
            audioSource.PlayOneShot(ThunderClip);
            audioSource.PlayOneShot(YoshiHurtClip);

            // Make burning yoshi follow the path
            StartCoroutine("MoveBurningYoshi", burningYoshi);
        }
    }

    private IEnumerator MoveBurningYoshi(GameObject burningYoshi)
    {
        int waypointID = 0;
        float speed = 0.3f;
        while (true)
        {
            // Moves towards next waypoint
            Vector3 nextWaypoint = Waypoints[waypointID];
            Vector3 directionVector = nextWaypoint - burningYoshi.transform.position;
            Vector3 movingVector = directionVector.normalized * speed;
            burningYoshi.transform.position += movingVector;

            // Make sure player yoshi is at the same place
            PlayerYoshi.transform.position = burningYoshi.transform.position;

            // If we are in the vicinity of next waypoint
            if (directionVector.magnitude < 0.5f)
            {
                if (waypointID == Waypoints.Count - 1)
                    break;
                else
                    waypointID++;
            }

            yield return new WaitForSeconds(1f / 60f);
        }

        // Put Yoshi back
        PlayerYoshi.SetActive(true);
        Destroy(burningYoshi);

        // Play mama mia
        audioSource.PlayOneShot(YoshiMamaMiaClip);
    }

    private void OnDrawGizmosSelected()
    {
        float radiusOfDot = 0.1f;
        //if you want to add spacing, just iterate i = i+2
        for (int i = 0; i < Waypoints.Count; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Waypoints[i], radiusOfDot);

            if (i > 0)
                Gizmos.DrawLine(Waypoints[i - 1], Waypoints[i]);
        }
    }
}
