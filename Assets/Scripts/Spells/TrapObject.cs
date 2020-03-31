using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public void Activate()
    {
        InputManager.s_Instance.gameObject.transform.position = transform.position;
        Destroy(gameObject);
    }
}
