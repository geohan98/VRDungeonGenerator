﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "DunegeonGenerator/Room")]
public class DG_Room : ScriptableObject
{
    public Vector2Int m_Size;
    public List<DG_Door> m_Doors;
    public List<Vector2Int> m_EmptyCells;
    public GameObject m_Prefab;
    public List<GameObject> m_DoorPrefabs;
}
