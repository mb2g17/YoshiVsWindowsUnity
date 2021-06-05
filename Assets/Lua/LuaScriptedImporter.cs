﻿#if UNITY_EDITOR

using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using System.IO;

[ScriptedImporter(1, "lua")]
public class LuaScriptedImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset lua = new TextAsset(File.ReadAllText(ctx.assetPath));
        ctx.AddObjectToAsset("main", lua);
        ctx.SetMainObject(lua);
    }
}
#endif