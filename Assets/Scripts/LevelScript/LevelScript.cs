using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public static LevelScript s_instance;

    private void Awake()
    {
        if (s_instance != null)
        {
            Destroy(this);
        }
        s_instance = this;
    }

    public GameObject m_Player; //Ref To Player Instance
    public GameObject m_PlayerPrefab; //Prefab of Player Class

    private void Start()
    {
        if (m_Player == null)
        {
            //Spawn Player
            SpawnPoint spawnPoint = FindObjectOfType<SpawnPoint>();
            if (spawnPoint != null)
            {
                //Spawn At First Spawn Point
            }
            else
            {
                //Spawn At 0,0,0
            }
        }
    }
}
