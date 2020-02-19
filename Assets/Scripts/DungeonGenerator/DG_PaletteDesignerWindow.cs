using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DG_PaletteDesignerWindow : EditorWindow
{
    #region Private Variables
    private string m_paletteName = "Palette Name";
    private Object roomPrefab;
    private Object doorPrefab;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (!(roomPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/RoomVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Room Volume Prefab");
        if (!(doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DoorVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Door Volume Prefab");
    }
    #endregion

    #region Public Functions
    [MenuItem("Dungeon Generator/Palette Designer")]
    public static void ShowWindow()
    {
        GetWindow<DG_PaletteDesignerWindow>("Palette Designer");
    }
    #endregion

    #region Private Functions
    private void OnGUI()
    {
        m_paletteName = GUILayout.TextField(m_paletteName, 128);

        if (GUILayout.Button("Create Palette", GUILayout.ExpandWidth(false)))
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

        if (GUILayout.Button("Add Room", GUILayout.ExpandWidth(false)))
        {
            Selection.activeObject = Instantiate(roomPrefab);
        }
        if (GUILayout.Button("Add Door", GUILayout.ExpandWidth(false)))
        {
            Selection.activeObject = Instantiate(doorPrefab);
        }
        if (GUILayout.Button("Debug Function", GUILayout.ExpandWidth(false)))
        {

        }
    }
    private List<DG_RoomVolume> GetAllRooms()
    {
        DG_RoomVolume[] array = FindObjectsOfType<DG_RoomVolume>();
        List<DG_RoomVolume> list = new List<DG_RoomVolume>();
        foreach (DG_RoomVolume roomVolume in array)
        {
            list.Add(roomVolume);
        }
        Log("Found " + list.Count + " Rooms");
        return list;
    }
    private void Log(string _msg)
    {
        Debug.Log("[Palette Designer][Info]:" + _msg);
    }
    private void LogWarning(string _msg)
    {
        Debug.Log("[Palette Designer][Warning]:" + _msg);
    }
    #endregion
}
