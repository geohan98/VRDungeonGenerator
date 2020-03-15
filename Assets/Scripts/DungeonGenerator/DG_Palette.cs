using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Palette", menuName = "DunegeonGenerator/Palette")]
public class DG_Palette : ScriptableObject
{
    public List<DG_Room> m_Rooms = new List<DG_Room>();
    public GameObject m_ConnectionPrefab;

    public void Reset()
    {
        m_Rooms = new List<DG_Room>();
        m_ConnectionPrefab = null;
    }
}
