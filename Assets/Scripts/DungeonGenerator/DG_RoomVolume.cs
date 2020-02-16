using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_RoomVolume : MonoBehaviour
{
    public static bool m_Debug = true;
    public Vector3Int m_Size;
    private DG_Room m_instance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(m_Size.x / 2.0f, m_Size.y / 2.0f, m_Size.z / 2.0f), new Vector3(m_Size.x, m_Size.y, m_Size.z));
    }

    public List<GameObject> CompileObjectsInsideBounds()
    {
        Bounds bounds = new Bounds(transform.position + new Vector3(m_Size.x / 2.0f, m_Size.y / 2.0f, m_Size.z / 2.0f), new Vector3(m_Size.x, m_Size.y, m_Size.z));

        List<GameObject> objects = new List<GameObject>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject item in allObjects)
        {
            if (bounds.Contains(item.transform.position) && !item.GetComponent<DG_RoomVolume>())
            {
                Log("Found GameObject: " + item.name);
                objects.Add(item);
            }
        }
        return objects;
    }

    private void Log(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[Room Volume][Info]:" + _msg);
    }
    private void LogWarning(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[Room Volume][Warning]:" + _msg);
    }

}
