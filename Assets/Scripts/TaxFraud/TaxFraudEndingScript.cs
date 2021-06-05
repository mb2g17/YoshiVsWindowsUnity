using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxFraudEndingScript : MonoBehaviour
{
    private EnemyTextScript enemyTextScript;

    /// <summary>
    /// Background object
    /// </summary>
    public GameObject Background;

    /// <summary>
    /// New sprite to set
    /// </summary>
    public Sprite NewBackground;

    public AudioSource AudioSource;
    public AudioClip BGM;

    // Start is called before the first frame update
    void Start()
    {
        enemyTextScript = FindObjectOfType<EnemyTextScript>();
        enemyTextScript.OnAllEnemiesDead = () =>
        {
            Background.GetComponent<SpriteRenderer>().sprite = NewBackground;
            AudioSource.Stop();
            AudioSource.clip = BGM;
            AudioSource.Play();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
