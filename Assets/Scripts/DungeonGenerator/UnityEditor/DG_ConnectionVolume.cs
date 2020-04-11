using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[SelectionBase]
public class DG_ConnectionVolume : MonoBehaviour
{
    public float m_Height = 1.0f;
    public List<Transform> GetConnectionObjects()
    {
        List<Transform> transforms = new List<Transform>();
        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(1.0f, m_Height, 1.0f));

        GameObject[] AllGameObjects = EditorSceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject gameObject in AllGameObjects)
        {
            if (bounds.Contains(gameObject.transform.position))
            {
                if (!(gameObject.GetComponent<DG_RoomVolume>() || gameObject.GetComponent<DG_DoorVolume>() || gameObject.GetComponent<DG_EmptyCell>() || gameObject.GetComponent<DG_ConnectionVolume>()))
                {
                    transforms.Add(gameObject.transform);
                }
            }
        }
        return transforms;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f), new Vector3(1, m_Height, 1));
    }
}
