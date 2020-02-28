using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_RoomVolume : MonoBehaviour
{
    #region Public Variables
    public static bool m_Debug = true;
    public Vector2Int m_Size;
    #endregion

    #region Private Variables
    private List<DG_Door> m_Doors;
    #endregion

    #region Public Functions
    public void UpdateRoomData()
    {
        m_Doors = new List<DG_Door>();

        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, 1.0f, m_Size.y));

        DG_DoorVolume[] doorVolumes = FindObjectsOfType<DG_DoorVolume>();

        foreach (DG_DoorVolume door in doorVolumes)
        {
            if (bounds.Contains(door.transform.position))
            {
                m_Doors.Add(new DG_Door(DoorWorldPositionToRelativeGridPosition(door), DoorToDirection(door)));
            }
        }
    }
    public DG_Room GetRoomData()
    {
        UpdateRoomData();
        DG_Room room = new DG_Room();
        room.m_Size = new Vector2Int(m_Size.x, m_Size.y);
        room.m_Doors = m_Doors;
        return room;
    }
    #endregion

    #region Private Functions
    private DG_Direction DoorToDirection(DG_DoorVolume _door)
    {
        if (_door.transform.rotation.eulerAngles.y > -5.0f && _door.transform.rotation.eulerAngles.y < 5.0f)
        {
            return DG_Direction.North;
        }
        if (_door.transform.rotation.eulerAngles.y > 85.0f && _door.transform.rotation.eulerAngles.y < 95.0f)
        {
            return DG_Direction.East;
        }
        if (_door.transform.rotation.eulerAngles.y > 175.0f && _door.transform.rotation.eulerAngles.y < 185.0f)
        {
            return DG_Direction.South;
        }
        if (_door.transform.rotation.eulerAngles.y > 265.0f && _door.transform.rotation.eulerAngles.y < 275.0f)
        {
            return DG_Direction.West;
        }
        LogWarning("Door Not Aligned" + _door.ToString());
        return DG_Direction.None;
    }
    private Vector2Int DoorWorldPositionToRelativeGridPosition(DG_DoorVolume _Door)
    {
        Vector3 relativeWorldPosition = _Door.transform.position - transform.position;
        return new Vector2Int(Mathf.RoundToInt(relativeWorldPosition.x), Mathf.RoundToInt(relativeWorldPosition.z));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, 1.0f, m_Size.y));
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
