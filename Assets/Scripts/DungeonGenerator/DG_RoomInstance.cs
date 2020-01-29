using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DG_RoomInstance
{
    public Vector2Int m_Position;
    public DG_Room m_Room;
    public List<DG_Door> m_ConnectedDoors;
}
