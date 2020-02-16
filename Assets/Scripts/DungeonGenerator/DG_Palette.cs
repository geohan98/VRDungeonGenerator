using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Palette", menuName = "DunegeonGenerator/Palette")]
public class DG_Palette : ScriptableObject
{
    public List<DG_Room> m_Rooms;
    public GameObject m_ConnectionPrefab;
}
