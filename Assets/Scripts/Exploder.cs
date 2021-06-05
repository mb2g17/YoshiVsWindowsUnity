using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When Yoshi touches this Exploder, this object will explode
/// </summary>
public class Exploder : MonoBehaviour
{
    public AudioClip explodeClip;
    public GameObject explosion;

    private Edible edible;

    private void Start()
    {
        edible = GetComponent<Edible>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Yoshi>() != null && enabled)
        {
            // Creates explosion
            GameObject explode = Instantiate(explosion);
            explode.transform.position = transform.position;

            // Plays sound
            collision.GetComponent<AudioSource>().PlayOneShot(explodeClip);

            // Runs events
            edible.CaughtEvent();
            edible.EatenEvent();

            // Destroy self
            Destroy(gameObject);
        }
    }
}
