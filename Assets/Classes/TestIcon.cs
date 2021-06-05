using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace Fungus
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class HierarchyIcons
    {
        // Mapping of types to icons
        static Dictionary<Type, Texture2D> TextureMapping = new Dictionary<Type, Texture2D>()
        {
            /*{ typeof(AudioSource), (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Sprites/SupertoadSprite.fw.png", typeof(Texture2D)) },
            { typeof(MainMenuScript), (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Sprites/goomba.fw.png", typeof(Texture2D)) },
            { typeof(VideoPlayer), (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Sprites/document.fw.png", typeof(Texture2D)) },
            { typeof(Camera), (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Sprites/mine.fw.png", typeof(Texture2D)) },*/
        };

        static bool initalHierarchyCheckFlag = true;

        static HierarchyIcons()
        {
            initalHierarchyCheckFlag = true;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyIconCallback;
#if UNITY_2018_1_OR_NEWER
            EditorApplication.hierarchyChanged += HierarchyChanged;
#else
            EditorApplication.hierarchyWindowChanged += HierarchyChanged;
#endif
        }

        static Dictionary<int, Texture2D> IDToTexture = new Dictionary<int, Texture2D>();

        //track all gameobjectIds that have flowcharts on them
        static void HierarchyChanged()
        {
            IDToTexture.Clear();

            // For each type
            foreach (Type type in TextureMapping.Keys)
            {
                // Get a list of objects of that type
                var objects = GameObject.FindObjectsOfType(type);
                List<int> IDs = objects.Select(x => ((Behaviour)x)
                    .gameObject.GetInstanceID()).Distinct().ToList();

                // Add ID to texture mapping
                foreach (int id in IDs)
                    IDToTexture.Add(id, TextureMapping[type]);
            }
        }

        //Draw icon if the isntance id is in our cached list
        static void HierarchyIconCallback(int instanceID, Rect selectionRect)
        {
            if (initalHierarchyCheckFlag)
            {
                HierarchyChanged();
                initalHierarchyCheckFlag = false;
            }

            // place the icon to the left of the element
            Rect r = new Rect(selectionRect);
#if UNITY_2019_1_OR_NEWER
            r.x -= r.height;
#else
            r.x = 0;
#endif
            r.width = r.height;

            //GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            //binary search as it is much faster to cache and int bin search than GetComponent
            //  should be less GC too
            if (IDToTexture.Keys.Contains(instanceID))
                GUI.Label(r, IDToTexture[instanceID]);
        }
    }
#endif
}
