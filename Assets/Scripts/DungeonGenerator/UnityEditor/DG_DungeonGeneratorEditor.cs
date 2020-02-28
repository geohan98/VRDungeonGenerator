using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DG_DungeonGenerator))]
public class DG_DungeonGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DG_DungeonGenerator myTarget = (DG_DungeonGenerator)target;

        Handles.DrawLine(Vector3.zero, Vector3.up * 5);

        if (GUILayout.Button("Build New Dungeon"))
        {
            myTarget.ClearDungeonData();
            myTarget.ClearDungeonMeshes();
            myTarget.GenerateDungeon();
            myTarget.BuildDunegon();
            myTarget.DebugCells();
        }

        if (GUILayout.Button("Generate New Dungeon"))
        {
            myTarget.ClearDungeonData();
            myTarget.GenerateDungeon();
            myTarget.DebugCells();
        }

        if (GUILayout.Button("Generate Dungeon Layout"))
        {
            myTarget.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Dungeon"))
        {
            myTarget.ClearDungeonData();
            myTarget.ClearDungeonMeshes();
        }

        if (GUILayout.Button("Clear Dungeon Meshes"))
        {
            myTarget.ClearDungeonMeshes();
        }

        if (GUILayout.Button("Debug Cells"))
        {
            myTarget.DebugCells();
        }

        DrawDefaultInspector();
    }
}
