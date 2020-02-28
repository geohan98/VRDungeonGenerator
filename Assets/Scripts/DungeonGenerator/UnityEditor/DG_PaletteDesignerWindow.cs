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
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        m_paletteName = GUILayout.TextField(m_paletteName, 128);

        if (GUILayout.Button("Create Palette", GUILayout.ExpandWidth(false)))
        {
            BuildDirectiory();
            AssetDatabase.CreateAsset(new DG_Palette(), "Assets/DungeonData/Palette_" + m_paletteName + "/Palette_" + m_paletteName + ".asset");
        }

        if (GUILayout.Button("Save Palette", GUILayout.ExpandWidth(false)))
        {
            BuildDirectiory();

            DG_Palette palette = (DG_Palette)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Palette_" + m_paletteName + "/Palette_" + m_paletteName + ".asset", typeof(DG_Palette));
            EditorUtility.SetDirty(palette);

            List<DG_RoomVolume> rooms = GetAllRooms();

            for (int i = 0; i < rooms.Count; i++)
            {
                AssetDatabase.CreateAsset(rooms[i].GetRoomData(), "Assets/DungeonData/Palette_" + m_paletteName + "/" + m_paletteName + "_Room_" + i + ".asset");
                if (i == 0) palette.m_Rooms = new List<DG_Room>();
                palette.m_Rooms.Add((DG_Room)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Palette_" + m_paletteName + "/" + m_paletteName + "_Room_" + i + ".asset", typeof(DG_Room)));
            }

            AssetDatabase.SaveAssets();
        }

        if (GUILayout.Button("Delete Palette", GUILayout.ExpandWidth(false)))
        {
            if (AssetDatabase.IsValidFolder("Assets/DungeonData/Palette_" + m_paletteName))
            {
                AssetDatabase.DeleteAsset("Assets/DungeonData/Palette_" + m_paletteName);
            }
        }

        GUILayout.Space(20.0f);

        if (GUILayout.Button("Add Room", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(roomPrefab);
        }
        if (GUILayout.Button("Add Door", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(doorPrefab);
        }

        GUILayout.Label("Number Of Rooms In Scene: " + FindObjectsOfType<DG_RoomVolume>().Length.ToString(), style);

    }
    private void CheckVolumePrefabs()
    {
        if (!(roomPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_RoomVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Room Volume Prefab");
        if (!(doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_DoorVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Door Volume Prefab");
    }
    private void BuildDirectiory()
    {
        if (!AssetDatabase.IsValidFolder("Assets/DungeonData"))
        {
            AssetDatabase.CreateFolder("Assets", "DungeonData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/DungeonData/Palette_" + m_paletteName))
        {
            AssetDatabase.CreateFolder("Assets/DungeonData", "Palette_" + m_paletteName);
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
