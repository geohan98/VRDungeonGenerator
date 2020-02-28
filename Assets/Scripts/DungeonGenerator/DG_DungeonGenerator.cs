using System.Collections.Generic;
using UnityEngine;

public class DG_DungeonGenerator : MonoBehaviour
{
    #region Public Variables
    public int m_Seed;
    public bool m_Seeded = false;
    public bool m_Debug;
    public int m_NumberOfRooms;
    public Vector2Int m_ConnectionLength;
    public DG_Palette m_Pallete;
    public int m_RoomPadding = 0;
    public float m_DrawSquareSize = 0.45f;
    public float m_DrawSquareDuration = 10.0f;
    public int m_MaxFails = 100;
    #endregion

    #region Private Variables
    private Dictionary<Vector2Int, DG_CellType> m_Cells = new Dictionary<Vector2Int, DG_CellType>();
    private List<DG_RoomInstance> m_RoomInstances = new List<DG_RoomInstance>();
    private List<DG_Connection> m_Connections = new List<DG_Connection>();
    private int m_FailCount;
    private int m_RoomCount;
    #endregion

    #region Unity Functions
    private void OnEnable()
    {

    }
    private void Awake()
    {
    }
    private void Start()
    {
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {

    }
    #endregion

    #region Public Functions
    public void DebugCells()
    {
        foreach (Vector2Int cell in m_Cells.Keys)
        {
            Color color = Color.clear;
            switch (m_Cells[cell])
            {
                case DG_CellType.Room:
                    color = Color.red;
                    break;
                case DG_CellType.Door:
                    color = Color.cyan;
                    break;
                case DG_CellType.Connection:
                    color = Color.green;
                    break;
            }
            DrawSquare(cell, Vector2Int.one, color);
        }
    }
    public void DebugRooms()
    {
        foreach (DG_RoomInstance room in m_RoomInstances)
        {
            DrawSquare(room.m_Position, room.m_Room.m_Size, Color.red);
        }
    }
    public void DebugConnections() { }
    public void GenerateDungeon()
    {
        if (m_Pallete == null)
        {
            return;
        }

        if (m_NumberOfRooms == 0)
        {
            LogWarning("Room Count Is Zero");
            return;
        }

        if (!AddRandomSpawnRoom())
        {
            LogWarning("Failed To Add Random Spawn Room");
            return;
        }

        bool loop = true;
        while (loop)
        {
            if (m_FailCount >= m_MaxFails)
            {
                LogWarning("Max Fails Reached");
                loop = false;
                break;
            }

            if (m_RoomCount >= m_NumberOfRooms)
            {
                Log("All Rooms Added");
                loop = false;
                break;
            }

            if (AddRandomRoom())
            {
                m_RoomCount++;
                m_FailCount = 0;
            }
            else
            {
                m_FailCount++;
            }
        }
    }
    public void BuildDunegon()
    {
        BuildRoomMeshes();
        BuildConnectionMeshes();
    }
    public void ClearDungeonData()
    {
        m_Cells = new Dictionary<Vector2Int, DG_CellType>();
        m_RoomInstances = new List<DG_RoomInstance>();
        m_Connections = new List<DG_Connection>();
        m_RoomCount = 0;
        m_FailCount = 0;
    }
    public void ClearDungeonMeshes()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    #endregion

    #region Private Functions
    private bool AddRandomRoom()
    {
        if (m_Pallete.m_Rooms.Count < 1)
        {
            LogWarning("No Rooms In Palette");
            return false;
        }
        if (m_RoomInstances.Count < 1)
        {
            LogWarning("There Are No Room Instances");
            return false;
        }
        DG_RoomInstance startRoomInstance = m_RoomInstances[RandomNumberInRange(m_RoomInstances.Count)];
        if (!(startRoomInstance.m_ConnectedDoors.Count < 4))
        {
            Log("Start Room Has Too Many Connections");
            return false;
        }
        if (startRoomInstance.m_Room == null)
        {
            LogWarning("Start Room Has No Room Info");
            return false;
        }
        DG_Room startRoomData = startRoomInstance.m_Room;
        if (startRoomData.m_Doors.Count < 1)
        {
            LogWarning("Start Room Has No Doors");
            return false;
        }
        DG_Door exitDoor = startRoomData.m_Doors[RandomNumberInRange(startRoomData.m_Doors.Count)];
        DG_Room newRoom = m_Pallete.m_Rooms[RandomNumberInRange(m_Pallete.m_Rooms.Count)];
        if (newRoom.m_Doors.Count < 0)
        {
            LogWarning("New Room Has No Doors");
            return false;
        }
        List<DG_Door> potentialDoors = FilterDoorsByDirection(newRoom.m_Doors, GetOpositeDirection(exitDoor.m_Direction));
        if (potentialDoors.Count < 1)
        {
            LogWarning("New Room Has No Potential Doors");
            return false;
        }

        DG_Door entryDoor = potentialDoors[RandomNumberInRange(potentialDoors.Count)];

        int connectionLength = RandomNumberInRange(m_ConnectionLength.y, m_ConnectionLength.x);

        Vector2Int connectionStart = Vector2Int.zero, connectionEnd = Vector2Int.zero, newRoomLocation = Vector2Int.zero;

        if (connectionLength > 1)
        {
            connectionStart = startRoomInstance.m_Position + exitDoor.m_Position + DirectionToVector(exitDoor.m_Direction);
            connectionEnd = connectionStart + DirectionToVector(exitDoor.m_Direction) * connectionLength;
            newRoomLocation = connectionEnd + DirectionToVector(exitDoor.m_Direction) - entryDoor.m_Position;
        }
        else if (connectionLength == 1)
        {
            connectionStart = startRoomInstance.m_Position + exitDoor.m_Position + DirectionToVector(exitDoor.m_Direction);
            connectionEnd = connectionStart;
            newRoomLocation = connectionEnd + DirectionToVector(exitDoor.m_Direction) - entryDoor.m_Position;
        }
        else if (connectionLength == 0)
        {
            newRoomLocation = startRoomInstance.m_Position + exitDoor.m_Position + DirectionToVector(exitDoor.m_Direction) - entryDoor.m_Position;
        }


        if (!CheckRoomOverlap(newRoom, newRoomLocation))
        {
            Log("Added Room");
            AddRoomToDungeonData(newRoom, newRoomLocation);
            startRoomInstance.m_ConnectedDoors.Add(exitDoor);
            m_RoomInstances[m_RoomInstances.Count - 1].m_ConnectedDoors.Add(entryDoor);

            if (connectionLength > 0)
            {
                AddConnectionToDungeonData(connectionStart, exitDoor.m_Direction, connectionLength);
            }

            return true;
        }

        return false;
    }
    private bool AddRandomSpawnRoom()
    {
        if (m_Pallete.m_Rooms.Count < 1)
        {
            LogWarning("No Rooms In Pallete");
            return false;
        }

        AddRoomToDungeonData(m_Pallete.m_Rooms[RandomNumberInRange(m_Pallete.m_Rooms.Count)], Vector2Int.zero);
        m_RoomCount++;
        Log("Spawn Room Added To Dungeon Data");
        return true;
    }
    private void AddRoomToDungeonData(DG_Room _Room, Vector2Int _Position)
    {
        for (int i = _Position.x - Mathf.FloorToInt(_Room.m_Size.x / 2); i <= _Position.x + Mathf.FloorToInt(_Room.m_Size.x / 2); i++)
        {
            for (int j = _Position.y - Mathf.FloorToInt(_Room.m_Size.y / 2); j <= _Position.y + Mathf.FloorToInt(_Room.m_Size.y / 2); j++)
            {
                m_Cells[new Vector2Int(i, j)] = DG_CellType.Room;
            }
        }

        foreach (DG_Door door in _Room.m_Doors)
        {
            m_Cells[new Vector2Int(_Position.x + door.m_Position.x, _Position.y + door.m_Position.y)] = DG_CellType.Door;
        }

        DG_RoomInstance tmp = new DG_RoomInstance();
        tmp.m_ConnectedDoors = new List<DG_Door>();
        tmp.m_Position = _Position;
        tmp.m_Room = _Room;
        m_RoomInstances.Add(tmp);
    }
    private void AddConnectionToDungeonData(Vector2Int _StartPosition, DG_Direction _Direction, int _Length)
    {
        m_Connections.Add(new DG_Connection(_StartPosition, _Direction, _Length));

        if (_Length == 1)
        {
            m_Cells[_StartPosition] = DG_CellType.Connection;
            return;
        }

        if (_Direction == DG_Direction.North || _Direction == DG_Direction.East)
        {
            for (int i = _StartPosition.x; i <= _StartPosition.x + (DirectionToVector(_Direction).x * _Length); i++)
            {
                for (int j = _StartPosition.y; j <= _StartPosition.y + (DirectionToVector(_Direction).y * _Length); j++)
                {
                    m_Cells[new Vector2Int(i, j)] = DG_CellType.Connection;
                }
            }
        }
        else
        {
            for (int i = _StartPosition.x; i >= _StartPosition.x + (DirectionToVector(_Direction).x * _Length); i--)
            {
                for (int j = _StartPosition.y; j >= _StartPosition.y + (DirectionToVector(_Direction).y * _Length); j--)
                {
                    m_Cells[new Vector2Int(i, j)] = DG_CellType.Connection;
                }
            }
        }
    }
    private void BuildRoomMeshes()
    {
        foreach (DG_RoomInstance roomInstance in m_RoomInstances)
        {
            if (roomInstance.m_Room.m_Prefab != null)
            {
                Instantiate(roomInstance.m_Room.m_Prefab, GridToWorldAxis(roomInstance.m_Position), Quaternion.Euler(Vector3.zero), transform);
            }
        }
    }
    private void BuildConnectionMeshes()
    {
        if (m_Pallete.m_ConnectionPrefab == null)
        {
            return;
        }

        Log("Building Connections");
        foreach (DG_Connection connection in m_Connections)
        {
            Log(connection.m_Length.ToString());
            for (int i = 0; i <= connection.m_Length; i++)
            {
                Instantiate(m_Pallete.m_ConnectionPrefab, GridToWorldAxis(connection.m_StartPosition + i * DirectionToVector(connection.m_Direction)), Quaternion.Euler(DirectionToEulerRotation(connection.m_Direction)), transform);
            }
        }
    }
    private bool CheckRoomOverlap(DG_Room _Room, Vector2Int _Position)
    {
        Log("Checking For Room Overlap");
        for (int i = -Mathf.FloorToInt(_Room.m_Size.x / 2) - m_RoomPadding; i <= Mathf.FloorToInt(_Room.m_Size.x / 2) + m_RoomPadding; i++)
        {
            for (int j = -Mathf.FloorToInt(_Room.m_Size.y / 2) - m_RoomPadding; j <= Mathf.FloorToInt(_Room.m_Size.y / 2) + m_RoomPadding; j++)
            {
                if (m_Cells.ContainsKey(_Position + new Vector2Int(i, j)))
                {
                    Log("Overlap Found");
                    return true;
                }
            }
        }
        return false;
    }
    private void DrawSquare(Vector2Int _Position, Vector2Int _Size, Color _Color)
    {
        //Top
        Debug.DrawLine(new Vector3(_Position.x, 0, _Position.y) + new Vector3(-_Size.x, 0, _Size.y) * m_DrawSquareSize, new Vector3(_Position.x, 0, _Position.y) + new Vector3(_Size.x, 0, _Size.y) * m_DrawSquareSize, _Color, m_DrawSquareDuration);
        //Bottom
        Debug.DrawLine(new Vector3(_Position.x, 0, _Position.y) + new Vector3(-_Size.x, 0, -_Size.y) * m_DrawSquareSize, new Vector3(_Position.x, 0, _Position.y) + new Vector3(_Size.x, 0, -_Size.y) * m_DrawSquareSize, _Color, m_DrawSquareDuration);
        //Left
        Debug.DrawLine(new Vector3(_Position.x, 0, _Position.y) + new Vector3(-_Size.x, 0, _Size.y) * m_DrawSquareSize, new Vector3(_Position.x, 0, _Position.y) + new Vector3(-_Size.x, 0, -_Size.y) * m_DrawSquareSize, _Color, m_DrawSquareDuration);
        //Right
        Debug.DrawLine(new Vector3(_Position.x, 0, _Position.y) + new Vector3(_Size.x, 0, _Size.y) * m_DrawSquareSize, new Vector3(_Position.x, 0, _Position.y) + new Vector3(_Size.x, 0, -_Size.y) * m_DrawSquareSize, _Color, m_DrawSquareDuration);
    }
    int RandomNumberInRange(int max, int min = 0)
    {
        if (m_Seeded)
        {
            Random.InitState(m_Seed + m_Cells.Count + m_RoomInstances.Count + m_Connections.Count + m_FailCount);
        }

        if (min == max - 1)
        {
            return min;
        }
        return Random.Range(min, max);
    }
    Vector2Int DirectionToVector(DG_Direction _Direction)
    {
        Vector2Int value = Vector2Int.zero;

        switch (_Direction)
        {
            case DG_Direction.North:
                value = new Vector2Int(0, 1);
                break;
            case DG_Direction.East:
                value = new Vector2Int(1, 0);
                break;
            case DG_Direction.South:
                value = new Vector2Int(0, -1);
                break;
            case DG_Direction.West:
                value = new Vector2Int(-1, 0);
                break;
        }
        return value;
    }
    List<DG_Door> FilterDoorsByDirection(List<DG_Door> _Doors, DG_Direction _Direction)
    {
        List<DG_Door> doors = new List<DG_Door>();
        foreach (DG_Door door in _Doors)
        {
            if (door.m_Direction == _Direction)
            {
                doors.Add(door);
            }
        }
        return doors;
    }
    DG_Direction GetOpositeDirection(DG_Direction _Direction)
    {
        DG_Direction direction = DG_Direction.None;

        switch (_Direction)
        {
            case DG_Direction.North:
                direction = DG_Direction.South;
                break;
            case DG_Direction.East:
                direction = DG_Direction.West;
                break;
            case DG_Direction.South:
                direction = DG_Direction.North;
                break;
            case DG_Direction.West:
                direction = DG_Direction.East;
                break;
        }

        return direction;
    }
    Vector3 GridToWorldAxis(Vector2Int _gridPosition)
    {
        return new Vector3(_gridPosition.x, 0, _gridPosition.y);
    }
    float DirectionToZRotation(DG_Direction _Direction)
    {
        if (_Direction == DG_Direction.East || _Direction == DG_Direction.West)
        {
            return 90;
        }
        return 0;
    }
    Vector3 DirectionToEulerRotation(DG_Direction _Direction)
    {
        if (_Direction == DG_Direction.East || _Direction == DG_Direction.West)
        {
            return new Vector3(0, 90, 0);
        }
        return Vector3.zero;
    }
    private void Log(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[Dungeon Generator][Info]:" + _msg);
    }
    private void LogWarning(string _msg)
    {
        if (!m_Debug) return;
        Debug.Log("[Dungeon Generator][Warning]:" + _msg);
    }
    #endregion
}
