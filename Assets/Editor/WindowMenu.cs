using Assets.Classes.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    class WindowMenu
    {

        [MenuItem("Assets/Create/Lua Script")]
        public static void CreateLuaFile(MenuCommand menuCommand)
        {
            CodeTemplates.CreateFromTemplate(
                "Lua Script.lua",
                @"Assets/Templates/LuaTemplate.lua");
        }

        [MenuItem("Yoshi vs Windows/Cheats/Restore health")]
        public static void RestoreHealth()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Cheat enabled!");
            }
            else
            {
                Debug.LogError("Not in play mode.");
            }
        }

        [MenuItem("Yoshi vs Windows/Cheats/Unlock All Levels")]
        public static void UnlockAllLevels()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Cheat enabled!");
                Time.timeScale = 0.5f;
            }
            else
            {
                Debug.LogError("Not in play mode.");
            }
        }

        [MenuItem("Yoshi vs Windows/Debug/Stop time (toki wo tomare)")]
        public static void StopTime()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Stopped time!");
                Time.timeScale = 0;
            }
            else
                Debug.LogError("Not in play mode.");
        }

        [MenuItem("Yoshi vs Windows/Debug/Half time")]
        public static void HalfTime()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Halved time!");
                Time.timeScale = 0.5f;
            }
            else
                Debug.LogError("Not in play mode.");
        }

        [MenuItem("Yoshi vs Windows/Debug/Restore time")]
        public static void RestoreTime()
        {
            /*if (Application.isPlaying)
            {
                Debug.Log("Restored time!");
                Time.timeScale = 1;
            }
            else
                Debug.LogError("Not in play mode.");*/
        }

        [MenuItem("Yoshi vs Windows/Debug/Doubled time")]
        public static void DoubleTime()
        {
            if (Application.isPlaying)
            {
                Debug.Log("Doubled time!");
                Time.timeScale = 2;
            }
            else
                Debug.LogError("Not in play mode.");
        }
    }
}
