using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DG_PaletteDesignerWindow : EditorWindow
{
    [MenuItem("Dungeon Generator/Palette Designer")]
    public static void ShowWindow()
    {
        GetWindow<DG_PaletteDesignerWindow>("Palette Designer");
    }

    string m_paletteName;
    bool m_Debug = true;

    private void OnGUI()
    {
        m_paletteName = GUILayout.TextField(m_paletteName, 128);

        Object roomPrefab;
        if (!(roomPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/RoomVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Room Volume Prefab");
        Object doorPrefab;
        if (!(doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DoorVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Door Volume Prefab");

        if (GUILayout.Button("Create Palette"))
        {
            if (!AssetDatabase.IsValidFolder("Assets/DungeonData"))
            {
                AssetDatabase.CreateFolder("Assets", "DungeonData");
            }
            if (!AssetDatabase.IsValidFolder("Assets/DungeonData/Palettes"))
            {
                AssetDatabase.CreateFolder("Assets/DungeonData", "Palettes");
            }

            AssetDatabase.CreateAsset(new DG_Palette(), "Assets/DungeonData/Palettes/" + m_paletteName + ".asset");
        }

        if (GUILayout.Button("Add Room"))
        {
            Selection.activeObject = Instantiate(roomPrefab);
        }
        if (GUILayout.Button("Add Door"))
        {
            Selection.activeObject = Instantiate(doorPrefab);
        }
    }

    private void Log(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[PaletteDesigner][Info]:" + _msg);
    }
    private void LogWarning(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[PaletteDesigner][Warning]:" + _msg);
    }
}
