using UnityEngine;
using UnityEditor;
using System.IO;

namespace DevionGames.InventorySystem
{
    internal static class ContextMenu
    {
        [MenuItem("Assets/Create/Game Events/IntGameEvent")]
        private static void MiIntGameEvent()
        {
            var asset = ScriptableObject.CreateInstance<IntGameEvent>();
            GenerateGameEventAsset(asset);
        }

        private static void GenerateGameEventAsset(ScriptableObject asset)
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = Path.Combine(path, asset.GetType().Name + ".asset");

            ProjectWindowUtil.CreateAsset(asset, path);
        }
    }
}