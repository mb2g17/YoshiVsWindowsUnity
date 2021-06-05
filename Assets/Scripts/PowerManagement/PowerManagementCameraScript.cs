using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManagementCameraScript : MonoBehaviour
{
    /// <summary>
    /// The black cover
    /// </summary>
    public GameObject Black;

    /// <summary>
    /// The fog around the edges
    /// </summary>
    public GameObject Fog;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EnemyTextScript>().OnAllEnemiesDead = () =>
        {
            // Changes camera size
            Camera.main.orthographicSize = 5;

            // Stops all lights
            foreach (PowerManagementLight pml in FindObjectsOfType<PowerManagementLight>())
            {
                pml.StopAllCoroutines(); // Stop them from trying to turn the black cover off again
                pml.Enabled = false; // Stop them from being turned on
            }

            // Disables black cover and fog
            Black.SetActive(false);
            Fog.SetActive(false);
        };
    }
}
