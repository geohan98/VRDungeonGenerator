using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    private void Awake()
    {
        PlayerManager.s_Instance.SetPlayerPosition(transform.position);
        Destroy(gameObject);
    }
}
