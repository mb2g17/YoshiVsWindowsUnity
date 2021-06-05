using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the hull to throw a bunch of coins at Yoshi
/// </summary>
public class CoinThrower : MonoBehaviour
{
    /// <summary>
    /// Stores the coins that are being shot
    /// </summary>
    private Dictionary<GameObject, Vector2> coins = new Dictionary<GameObject, Vector2>();

    /// <summary>
    /// Stores coin scripts, for optimisation
    /// </summary>
    private Dictionary<GameObject, CoinScript> coinScripts = new Dictionary<GameObject, CoinScript>();

    /// <summary>
    /// The coin prefab
    /// </summary>
    public GameObject CoinPrefab;

    /// <summary>
    /// The sprite the hull changes to when tossing coins
    /// </summary>
    public Sprite HullCoinSprite;

    /// <summary>
    /// The point where coins are spat out of
    /// </summary>
    public Transform Nuzzle;

    /// <summary>
    /// Speed of coins
    /// </summary>
    public float CoinSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Create a list of coins to destroy
        List<GameObject> toDestroy = new List<GameObject>();

        // Goes through all coins
        foreach (GameObject coin in coins.Keys)
        {
            // Moves coin, if it's not hidden
            if (!coinScripts[coin].Hidden)
                coin.transform.Translate(coins[coin] * CoinSpeed * Time.timeScale * Time.deltaTime);

            // If the coin went too far, destroy it
            if (coin.transform.position.x < -10)
                toDestroy.Add(coin);
        }

        // Destroys coins
        foreach (GameObject coin in toDestroy)
        {
            coins.Remove(coin);
            coinScripts.Remove(coin);
            Destroy(coin);
        }
    }

    public void SpitCoin()
    {
        // Instantiate coin at nuzzle
        GameObject coin = Instantiate(CoinPrefab);
        coin.transform.position = Nuzzle.position;

        // Calculates direction vector
        Vector2 directionVector = new Vector2(-15, Random.Range(-8, 4)).normalized;

        // Adds coin to dictionary
        coins.Add(coin, directionVector);

        // Sets up coin script stuff
        CoinScript coinScript = coin.GetComponent<CoinScript>();
        coinScript.OnDestroy = () =>
        {
            coins.Remove(coin);
            coinScripts.Remove(coin);
        };
        coinScripts.Add(coin, coinScript);
    }
}
