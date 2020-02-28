using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_DoorVolume : MonoBehaviour
{
    public static bool m_Debug = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.one);
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
