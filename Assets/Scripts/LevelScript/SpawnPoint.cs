using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class SpawnPoint : MonoBehaviour
{
    public Transform getSpawnLocation()
    {
        return transform;
    }
    void isSpawnPointClear() { }
}
