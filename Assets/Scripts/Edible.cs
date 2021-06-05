using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Makes this object edible by Yoshi
/// </summary>
public class Edible : MonoBehaviour
{
    /// <summary>
    /// The tip, only follows if not null
    /// </summary>
    private Transform _tip = null;

    /// <summary>
    /// Event that runs when we're caught by the tongue
    /// </summary>
    public Action CaughtEvent = () => { };

    /// <summary>
    /// Event that runs when we've been eaten
    /// </summary>
    public Action EatenEvent = () => { };

    /// <summary>
    /// If true, we will explode on eat
    /// </summary>
    public bool ExplodeOnEat = false;

    /// <summary>
    /// Explode prefab
    /// </summary>
    public GameObject Explode;

    // Update is called once per frame
    void Update()
    {
        // If we're being eaten, go to the tip of the tongue
        if (BeingEaten())
        {
            transform.position = _tip.position;
        }
    }

    /// <summary>
    /// Gets if we're being eaten
    /// </summary>
    /// <returns>Returns true if we're being eaten</returns>
    public bool BeingEaten()
    {
        return _tip != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we're not being eaten
        if (!BeingEaten())
        {
            // If the other thing is a tongue AND we're not dead
            if (collision.gameObject.GetComponent<TongueScript>() != null)
            {
                // Attach ourselves to the tip
                _tip = collision.gameObject.transform.GetChild(1);
                gameObject.tag = "KillMe"; // Queue ourselves to be eaten

                // Run caught event
                CaughtEvent();
            }
        }
    }

    private void OnDestroy()
    {
        // If we're being eaten
        if (BeingEaten())
        {
            // If we should explode, explode
            if (ExplodeOnEat)
            {
                GameObject explode = Instantiate(Explode);
                explode.transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    explode.transform.position.z);
            }

            // Plays the eat sound clip
            FindObjectOfType<Yoshi>().PlayEatClip();

            // Run eaten event
            EatenEvent();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Edible)), CanEditMultipleObjects]
public class MyScriptEditor : Editor
{
    private SerializedProperty _explodeOnEat;
    private SerializedProperty _explode;

    private void OnEnable()
    {
        _explodeOnEat = serializedObject.FindProperty("ExplodeOnEat");
        _explode = serializedObject.FindProperty("Explode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_explodeOnEat);

        if (_explodeOnEat.boolValue)
            EditorGUILayout.PropertyField(_explode);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif