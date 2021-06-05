﻿using UnityEngine;
using UnityEditor;
using System.Collections;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010

#if UNITY_EDITOR
public class ReplaceGameObjects : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject useGameObject;
    public GameObject Replace;

    [MenuItem("Tools/Replace GameObjects")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
    }

    void OnWizardCreate()
    {
        Transform[] Replaces;
        Replaces = Replace.GetComponentsInChildren<Transform>();

        foreach (Transform t in Replaces)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(useGameObject);
            newObject.transform.position = t.position;
            newObject.transform.rotation = t.rotation;

            Destroy(t.gameObject);

        }

    }
}
#endif