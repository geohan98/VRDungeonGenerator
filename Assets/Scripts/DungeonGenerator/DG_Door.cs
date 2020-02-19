using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DG_Door
{
    public Vector2Int m_Position;
    public DG_Direction m_Direction;

    public DG_Door(Vector2Int _Position, DG_Direction _Direction)
    {
        m_Position = _Position;
        m_Direction = _Direction;
    }
}
