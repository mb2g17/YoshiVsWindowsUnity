using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behaviour of the recycle bin (document creator)
/// </summary>
public class DocumentCreatorScript : MonoBehaviour
{
    /// <summary>
    /// The document prefab to instantiate
    /// </summary>
    public GameObject DocumentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Start creating documents
        StartCoroutine("CreateDocument");
    }

    private IEnumerator CreateDocument()
    {
        while (true)
        {
            GameObject document = Instantiate(DocumentPrefab); // Create new document
            document.transform.position = transform.position; // Sets position to our position
            yield return new WaitForSeconds(0.8f); // Wait a bit
        }
    }
}
