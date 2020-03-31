using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSpawn : MonoBehaviour
{
    [Header("DestroyAfterSpawn Options")]
    #region Public Variables
    public float m_time = 0;
    public bool m_Debug = true;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Functions
    private void Awake()
    {
        Destroy(gameObject, m_time);
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[DestroyAfterSpawn][" + _msg + "]");
        }
    }
    #endregion
}
