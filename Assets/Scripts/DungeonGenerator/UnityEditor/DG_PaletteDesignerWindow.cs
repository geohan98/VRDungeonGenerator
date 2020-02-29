using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DG_PaletteDesignerWindow : EditorWindow
{
    #region Private Variables
    private Object roomPrefab;
    private Object doorPrefab;
    private Object emptyCellPrefab;
    #endregion

    #region Unity Functions
    #endregion

    #region Public Functions
    [MenuItem("Dungeon Generator/Palette Designer")]
    public static void ShowWindow()
    {
        DG_PaletteDesignerWindow window = GetWindow<DG_PaletteDesignerWindow>("Palette Designer");
        window.minSize = new Vector2(325.0f, 300.0f);
        window.ShowTab();
    }
    #endregion

    #region Private Functions
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        GUILayout.Space(10.0f);
        GUILayout.BeginHorizontal();
        GUILayout.Space(10.0f);
        GUILayout.BeginVertical();

        if (GUILayout.Button("New Palette", GUILayout.ExpandWidth(false)))
        {
            DG_PaletteNameWindow.Init();
        }

        GUILayout.Space(5.0f);
        if (GUILayout.Button("Save Palette", GUILayout.ExpandWidth(false)))
        {
            DG_Palette palette = (DG_Palette)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + ".asset", typeof(DG_Palette));
            EditorUtility.SetDirty(palette);

            List<DG_RoomVolume> rooms = GetAllRooms();

            for (int i = 0; i < rooms.Count; i++)
            {
                AssetDatabase.CreateAsset(rooms[i].GetRoomData(), "Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_" + i + ".asset");
                if (i == 0) palette.m_Rooms = new List<DG_Room>();
                palette.m_Rooms.Add((DG_Room)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_" + i + ".asset", typeof(DG_Room)));
            }

            AssetDatabase.SaveAssets();
        }

        GUILayout.Space(5.0f);
        if (GUILayout.Button("Add Room", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(roomPrefab);
        }

        GUILayout.Space(5.0f);
        if (GUILayout.Button("Add Door", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(doorPrefab);
        }
        GUILayout.Space(5.0f);
        if (GUILayout.Button("Add Empty Cell", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(emptyCellPrefab);
        }

        GUILayout.Space(5.0f);
        GUILayout.Label("Number Of Rooms In Palette: " + FindObjectsOfType<DG_RoomVolume>().Length.ToString(), style);

        GUILayout.EndHorizontal();
        GUILayout.Space(10.0f);
        GUILayout.EndVertical();

    }
    private void CheckVolumePrefabs()
    {
        if (!(roomPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_RoomVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Room Volume Prefab");
        if (!(doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_DoorVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Door Volume Prefab");
        if (!(emptyCellPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_EmptyCell.prefab", typeof(GameObject)))) LogWarning("Failed To EmptyCell Prefab");
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
