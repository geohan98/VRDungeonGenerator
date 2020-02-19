using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_RoomVolume : MonoBehaviour
{
    #region Public Variables
    public static bool m_Debug = true;
    public Vector3Int m_Size;
    #endregion

    #region Private Variables
    private List<DG_Door> m_Doors;
    #endregion

    #region Public Functions
    public void UpdateRoomData()
    {
        m_Doors = new List<DG_Door>();

        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Size.y, m_Size.z));

        DG_DoorVolume[] doorVolumes = FindObjectsOfType<DG_DoorVolume>();

        foreach (DG_DoorVolume door in doorVolumes)
        {
            if (bounds.Contains(door.transform.position))
            {
                m_Doors.Add(new DG_Door(DoorWorldPositionToRelativeGridPosition(door), DG_Direction.None));
            }
        }
        Log("Found " + m_Doors.Count + "Doors");
    }
    #endregion

    #region Private Functions
    Vector2Int DoorWorldPositionToRelativeGridPosition(DG_DoorVolume _Door)
    {
        Vector3 relativeWorldPosition = transform.position - _Door.transform.position;
        return new Vector2Int(Mathf.RoundToInt(relativeWorldPosition.x), Mathf.RoundToInt(relativeWorldPosition.z));
    }
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
    #endregion
}
