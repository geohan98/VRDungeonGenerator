using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownRingControl : MonoBehaviour
{
    [Header("CooldownRingControl Options")]
    #region Public Variables
    public MeshRenderer m_Ring;
    public float m_Alpha;
    public Color m_ReadyColour;
    public Color m_RechargeColour;
    public bool m_SpellEquipped;
    public bool m_Debug = true;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Functions
    private void Update()
    {
        if (m_SpellEquipped)
        {
            m_Ring.material.SetColor("_EmissionColor", Color.Lerp(m_RechargeColour, m_ReadyColour, m_Alpha));
            if (m_Alpha >= 1)
            {
                m_Ring.material.SetColor("_EmissionColor", Color.white);
            }
        }
        else
        {
            m_Ring.material.SetColor("_EmissionColor", Color.clear);
        }
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[CooldownRingControl][" + _msg + "]");
        }
    }
    #endregion
}
