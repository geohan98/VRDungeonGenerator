#if (UNITY_EDITOR) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

[SelectionBase]
public class DG_RoomVolume : MonoBehaviour
{
    #region Public Variables
    public static bool m_Debug = true;
    public Vector2Int m_Size;
    public float m_Height = 1.0f;
    #endregion

    #region Private Variables
    private List<DG_Door> m_Doors;
    private List<DG_DoorVolume> m_DoorVolumes;
    private List<List<Transform>> m_DoorObjects;
    private List<Transform> m_objectsInRoom;
    private List<Vector2Int> m_EmptyCells;
    #endregion

    #region Public Functions
    public void UpdateRoomData()
    {
        m_Doors = new List<DG_Door>();
        m_EmptyCells = new List<Vector2Int>();

        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));

        DG_DoorVolume[] doorVolumes = FindObjectsOfType<DG_DoorVolume>();

        foreach (DG_DoorVolume door in doorVolumes)
        {
            if (bounds.Contains(door.transform.position))
            {
                m_Doors.Add(new DG_Door(WorldPositionToRelativeGridPosition(door.transform.position), DoorToDirection(door)));
            }
        }

        DG_EmptyCell[] emptyCells = FindObjectsOfType<DG_EmptyCell>();

        foreach (DG_EmptyCell cell in emptyCells)
        {
            if (bounds.Contains(cell.transform.position))
            {
                m_EmptyCells.Add(WorldPositionToRelativeGridPosition(cell.transform.position));
            }
        }
    }
    public DG_Room GetRoomData()
    {
        UpdateRoomData();
        DG_Room room = new DG_Room();
        room.m_Size = new Vector2Int(m_Size.x, m_Size.y);
        room.m_Doors = m_Doors;
        room.m_EmptyCells = m_EmptyCells;
        room.m_DoorPrefabs = new List<GameObject>();
        return room;
    }

    public List<Transform> GetRoomGameObjects()
    {
        List<Transform> transforms = new List<Transform>();
        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));

        GameObject[] AllGameObjects = EditorSceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject gameObject in AllGameObjects)
        {
            if (bounds.Contains(gameObject.transform.position))
            {
                if (!(gameObject.GetComponent<DG_RoomVolume>() || gameObject.GetComponent<DG_DoorVolume>() || gameObject.GetComponent<DG_EmptyCell>()))
                {
                    transforms.Add(gameObject.transform);
                }
            }
        }
        return transforms;
    }

    public void CompileRoomData()
    {
        List<Transform> objectsInRoom = GetAllObjectsInRoomBounds();
        List<DG_DoorVolume> doorsInRoom = GetAllDoorVolumesInBounds();
        Log(doorsInRoom.Count.ToString());
        List<Vector2Int> emptyCellsInRoom = GetAllEmptyCellsInBounds();

        List<Transform> objectsToRemove = new List<Transform>();
        List<List<Transform>> doorObjectsList = new List<List<Transform>>();

        foreach (DG_DoorVolume doorVolume in doorsInRoom)
        {
            List<Transform> doorObjects = new List<Transform>();

            Bounds doorBounds = CreateBoundsFromDoorVolume(doorVolume);

            foreach (Transform transform in objectsInRoom)
            {
                if (doorBounds.Contains(transform.position))
                {
                    objectsToRemove.Add(transform);
                    doorObjects.Add(transform);
                }
            }
            doorObjectsList.Add(doorObjects);
        }
        foreach (Transform transform in objectsToRemove)
        {
            objectsInRoom.Remove(transform);
        }
        m_DoorVolumes = doorsInRoom;
        m_objectsInRoom = objectsInRoom;
        m_DoorObjects = doorObjectsList;
    }
    public List<Transform> GetRoomObjects()
    {
        return m_objectsInRoom;
    }
    public List<List<Transform>> GetDoorObjects()
    {
        return m_DoorObjects;
    }
    public List<DG_DoorVolume> GetDoorVolumes()
    {
        return m_DoorVolumes;
    }

    #endregion

    #region Private Functions
    private List<DG_DoorVolume> GetAllDoorVolumesInBounds()
    {
        List<DG_DoorVolume> doorVolumesList = new List<DG_DoorVolume>();
        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));

        DG_DoorVolume[] doorVolumes = FindObjectsOfType<DG_DoorVolume>();

        foreach (DG_DoorVolume door in doorVolumes)
        {
            if (bounds.Contains(door.transform.position))
            {
                doorVolumesList.Add(door);
            }
        }
        return doorVolumesList;
    }
    private List<Vector2Int> GetAllEmptyCellsInBounds()
    {
        List<Vector2Int> emptyCellList = new List<Vector2Int>();
        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));

        DG_EmptyCell[] emptyCells = FindObjectsOfType<DG_EmptyCell>();

        foreach (DG_EmptyCell cell in emptyCells)
        {
            if (bounds.Contains(cell.transform.position))
            {
                emptyCellList.Add(WorldPositionToRelativeGridPosition(cell.transform.position));
            }
        }
        return emptyCellList;
    }
    private List<Transform> GetAllObjectsInRoomBounds()
    {
        List<Transform> transforms = new List<Transform>();
        Bounds bounds = new Bounds(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));

        GameObject[] AllGameObjects = EditorSceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject gameObject in AllGameObjects)
        {
            if (bounds.Contains(gameObject.transform.position))
            {
                if (!(gameObject.GetComponent<DG_RoomVolume>() || gameObject.GetComponent<DG_DoorVolume>() || gameObject.GetComponent<DG_EmptyCell>()))
                {
                    transforms.Add(gameObject.transform);
                }
            }
        }
        return transforms;
    }
    private Bounds CreateBoundsFromDoorVolume(DG_DoorVolume _door)
    {
        return new Bounds(_door.transform.position + Vector3.up * 0.5f, Vector3.one);
    }
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
    private Vector2Int WorldPositionToRelativeGridPosition(Vector3 _position)
    {
        Vector3 relativeWorldPosition = _position - transform.position;
        return new Vector2Int(Mathf.RoundToInt(relativeWorldPosition.x), Mathf.RoundToInt(relativeWorldPosition.z));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(m_Size.x, m_Height, m_Size.y));
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
#endif
