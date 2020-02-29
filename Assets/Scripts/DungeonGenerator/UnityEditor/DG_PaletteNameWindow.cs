using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DG_PaletteNameWindow : EditorWindow
{
    public static void Init()
    {
        DG_PaletteNameWindow window = ScriptableObject.CreateInstance<DG_PaletteNameWindow>();
        window.titleContent.text = "New Palette";
        window.maxSize = new Vector2(400.0f, 100.0f);
        window.minSize = new Vector2(400.0f, 100.0f);
        window.ShowUtility();
    }

    private string m_name = string.Empty;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enter Palette Name");

        GUILayout.Space(10.0f);

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Palette Name:", GUILayout.MaxWidth(90.0f));
        m_name = GUILayout.TextField(m_name, 128);

        GUILayout.Space(10.0f);

        GUILayout.EndHorizontal();

        GUILayout.Space(10.0f);

        GUILayout.BeginHorizontal();

        GUILayout.Space(50.0f);

        bool empty = m_name == string.Empty;

        EditorGUI.BeginDisabledGroup(empty);

        if (GUILayout.Button("Accept"))
        {
            BuildDirectiory();

            EditorSceneManager.SaveScene(EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single), "Assets/DungeonData/Palette_" + m_name + "/Palette_" + m_name + ".unity");
            AssetDatabase.CreateAsset(new DG_Palette(), "Assets/DungeonData/Palette_" + m_name + "/Palette_" + m_name + ".asset");
            this.Close();
        }

        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Cancel"))
        {
            this.Close();
        }

        GUILayout.Space(50.0f);

        GUILayout.EndHorizontal();
    }

    private void BuildDirectiory()
    {
        if (!AssetDatabase.IsValidFolder("Assets/DungeonData"))
        {
            AssetDatabase.CreateFolder("Assets", "DungeonData");
        }
        if (!AssetDatabase.IsValidFolder("Assets/DungeonData/Palette_" + m_name))
        {
            AssetDatabase.CreateFolder("Assets/DungeonData", "Palette_" + m_name);
        }
    }
}
