using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles money in Bill Gates boss
/// </summary>
public class MoneyScript : MonoBehaviour
{
    /// <summary>
    /// How fast money will move
    /// </summary>
    public float Speed = 2;

    /// <summary>
    /// Thunder clip
    /// </summary>
    public AudioClip ThunderClip;

    /// <summary>
    /// Explosion prefab
    /// </summary>
    public GameObject Explosion;

    /// <summary>
    /// Yoshi object
    /// </summary>
    private GameObject yoshi;

    /// <summary>
    /// Bill gates boss object
    /// </summary>
    private GameObject billGates;

    private BillGatesScript billGatesScript;

    /// <summary>
    /// The vector for us to go to
    /// </summary>
    private Vector2 directionVector;

    /// <summary>
    /// Current state
    /// 0 - Going to Yoshi
    /// 1 - Going to Bill Gates
    /// </summary>
    private int state = 0;

    private void Start()
    {
        yoshi = FindObjectOfType<Yoshi>().gameObject;
        billGatesScript = FindObjectOfType<BillGatesScript>();
        billGates = GameObject.FindGameObjectWithTag("BillGates");
        CalculateDirectionVector();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionVector * Time.timeScale * Time.deltaTime);

        // If we've gone too far
        if (transform.position.x < -10)
            Destroy(gameObject);
    }

    /// <summary>
    /// Calculates direction vector based on state
    /// </summary>
    private void CalculateDirectionVector()
    {
        directionVector = (
            (state == 0 ? yoshi : billGates).transform.position
            - transform.position
        ).normalized * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is tongue, change state and recalculate direction
        if (collision.GetComponent<TongueScript>() != null)
        {
            state = 1;
            CalculateDirectionVector();
        }

        // If this is Bill Gates AND we're chasing him
        if (collision.gameObject == billGates && state == 1)
        {
            // Use yoshi's audio to play clip
            yoshi.GetComponent<AudioSource>().PlayOneShot(ThunderClip);

            // Deduct health
            billGatesScript.Damage();

            // Explode
            GameObject explosion = Instantiate(Explosion);
            explosion.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                -5);

            // Destroy self
            Destroy(gameObject);
        }
    }
}
