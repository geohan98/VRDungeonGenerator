using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_RoomVolume : MonoBehaviour
{
    public static bool m_Debug = true;
    public Vector3Int m_Size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Size.y, m_Size.z));
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
