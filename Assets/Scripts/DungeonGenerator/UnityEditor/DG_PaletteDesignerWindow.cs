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
    private Object connectionPrefab;
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
            SaveRooms();
            SaveConnection();
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
        if (GUILayout.Button("Add Connection", GUILayout.ExpandWidth(false)))
        {
            CheckVolumePrefabs();
            Selection.activeObject = Instantiate(connectionPrefab);
        }

        GUILayout.Space(5.0f);
        GUILayout.Label("Number Of Rooms In Palette: " + FindObjectsOfType<DG_RoomVolume>().Length.ToString(), style);

        GUILayout.EndHorizontal();
        GUILayout.Space(10.0f);
        GUILayout.EndVertical();

    }
    private void SaveRooms()
    {
        //Gain A Referance To The Palette In The Project Folder
        DG_Palette currentPalette = (DG_Palette)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + ".asset", typeof(DG_Palette));
        currentPalette.Reset();
        //Set As A Dirty File That Needs To Be Saved On Next Save
        EditorUtility.SetDirty(currentPalette);
        //Create A List Of All Of The Room Volumes In The Scene
        List<DG_RoomVolume> roomVolumes = GetAllRoomVolumes();
        //Itterate Through All Of The Room Volumes In The Scene
        for (int i = 0; i < roomVolumes.Count; i++)
        {
            //Create A New Room Asset In The Project Folder Using The Data From The Room Volumes
            AssetDatabase.CreateAsset(roomVolumes[i].GetRoomData(), "Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_" + i + ".asset");
            //Gain A Referance To That New Room Volume
            DG_Room currentRoom = (DG_Room)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_" + i + ".asset", typeof(DG_Room));
            //Mark That Room Asset As Dirty So That All Changes Are Saved
            EditorUtility.SetDirty(currentRoom);
            //Create A New Gameobject With All Of The Objects Inside The Bounds Of The Room Volume
            roomVolumes[i].CompileRoomData();
            GameObject gameObject = CreateObjectFromTransformData(roomVolumes[i].GetRoomObjects(), roomVolumes[i].transform.position);
            //Create A New Prefab Inside The Project Folder Using The New GameObject
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_Prefab_" + i + ".prefab");
            //Destroy The GameObject In The Sceene To Remove Clutter
            DestroyImmediate(gameObject);

            List<List<Transform>> doorObjects = roomVolumes[i].GetDoorObjects();


            for (int j = 0; j < doorObjects.Count; j++)
            {
                GameObject doorObject = CreateObjectFromTransformData(doorObjects[j], roomVolumes[i].GetDoorVolumes()[j].transform.position);

                GameObject doorPrefab = PrefabUtility.SaveAsPrefabAsset(doorObject, "Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Room_Prefab_" + i + "_Door_Prefab" + j + ".prefab");

                currentRoom.m_DoorPrefabs.Add(doorPrefab);

                DestroyImmediate(doorObject);
            }

            //Set The Prefab Of The New Room Asset AS The New Prefab
            currentRoom.m_Prefab = prefab;
            //Add The Room To The Current Palette
            currentPalette.m_Rooms.Add(currentRoom);
            //Save All Changes
            AssetDatabase.SaveAssets();
        }
    }
    private void SaveConnection()
    {
        //Gain A Referance To The Palette In The Project Folder
        DG_Palette currentPalette = (DG_Palette)AssetDatabase.LoadAssetAtPath("Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + ".asset", typeof(DG_Palette));
        //Set As A Dirty File That Needs To Be Saved On Next Save
        EditorUtility.SetDirty(currentPalette);
        //Find The Connection Volume In The Scene
        DG_ConnectionVolume connectionVolume = FindObjectOfType<DG_ConnectionVolume>();
        //Check If The Refernece To The Connection Volume Is Valid
        if (connectionVolume == null)
        {
            LogWarning("No Connection Volume!");
            return;
        }
        //Create A New GameObject With All Of The Objects Inside The Conection Volume As Children
        GameObject connectionObject = CreateObjectFromTransformData(connectionVolume.GetConnectionObjects(), connectionVolume.transform.position);
        //Create A New Prefab In The Project Folder using the New Gameobject
        GameObject connectionPrefab = PrefabUtility.SaveAsPrefabAsset(connectionObject, "Assets/DungeonData/" + EditorSceneManager.GetActiveScene().name + "/" + EditorSceneManager.GetActiveScene().name + "_Connection.prefab");
        //Set The Connection Prefab Of The Current Palette As The New Prefab
        currentPalette.m_ConnectionPrefab = connectionPrefab;
        //Destroy The GameObject In The Sceene To Remove Clutter
        DestroyImmediate(connectionObject);
        //Save All Chnages To Dirty Objects
        AssetDatabase.SaveAssets();
    }
    private void CheckVolumePrefabs()
    {
        if (!(roomPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_RoomVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Room Volume Prefab");
        if (!(doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_DoorVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Door Volume Prefab");
        if (!(emptyCellPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_EmptyCell.prefab", typeof(GameObject)))) LogWarning("Failed To Load EmptyCell Prefab");
        if (!(connectionPrefab = AssetDatabase.LoadAssetAtPath("Assets/DungeonData/Prefabs/DG_ConnectionVolume.prefab", typeof(GameObject)))) LogWarning("Failed To Load Connection Prefab");
    }
    private List<DG_RoomVolume> GetAllRoomVolumes()
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
    private GameObject CreateObjectFromTransformData(List<Transform> _Transforms, Vector3 _RootOffset)
    {
        GameObject root = new GameObject();

        foreach (Transform transform in _Transforms)
        {
            Transform child = Instantiate(transform, root.transform);
            child.position -= _RootOffset;
        }

        return root;
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
