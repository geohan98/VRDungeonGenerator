using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public void Activate()
    {
        SpellManager.m_player.transform.position = transform.position;
        //Destroy(gameObject);
    }
}
