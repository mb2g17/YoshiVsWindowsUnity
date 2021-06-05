using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointerPlainsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EnemyTextScript>().OnAllEnemiesDead = () =>
        {
            SceneManager.LoadScene("PointerPlainsFinish");
        };
    }
}
