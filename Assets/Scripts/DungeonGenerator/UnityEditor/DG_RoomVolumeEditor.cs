using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DG_RoomVolume))]
public class DG_RoomVolumeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DG_RoomVolume myTarget = (DG_RoomVolume)target;

        if (GUILayout.Button("SaveRoom"))
        {
            if (!AssetDatabase.IsValidFolder("Assets/DungeonData"))
            {
                AssetDatabase.CreateFolder("Assets", "DungeonData");
            }
            if (!AssetDatabase.IsValidFolder("Assets/DungeonData/Rooms"))
            {
                AssetDatabase.CreateFolder("Assets/DungeonData", "Rooms");
            }

            myTarget.UpdateRoomData();
            AssetDatabase.CreateAsset(myTarget.GetRoomData(), "Assets/DungeonData/Rooms/" + "DebugRoom" + ".asset");
        }

        DrawDefaultInspector();
    }
}
