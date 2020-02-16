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

        if (GUILayout.Button("Compile Room"))
        {
            myTarget.CompileObjectsInsideBounds();
        }
        if (GUILayout.Button("Create Room Prefab"))
        {
            myTarget.CompileObjectsInsideBounds();
        }

        DrawDefaultInspector();
    }
}
