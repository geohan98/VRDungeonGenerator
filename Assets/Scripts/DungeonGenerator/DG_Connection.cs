using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DG_Connection
{
    public Vector2Int m_StartPosition;
    public DG_Direction m_Direction;
    public int m_Length;

    public DG_Connection(Vector2Int _StartPosition, DG_Direction _Direction, int _Length)
    {
        this.m_StartPosition = _StartPosition;
        this.m_Direction = _Direction;
        this.m_Length = _Length;
    }

}
