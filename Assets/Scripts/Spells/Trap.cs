using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Spells/Trap")]
public class Trap : Spell
{

    #region Public Variables
    [Header("Trap Attributes")]
    public float m_Range;
    public LayerMask m_LayerMask;
    public GameObject m_TargetPrefab;
    public GameObject m_TrapPrefab;
    public bool m_Debug = false;
    #endregion

    #region Private Varaibles
    bool m_hit;
    Vector3 m_hitPos = Vector3.zero;
    GameObject m_target;
    GameObject m_trap;
    #endregion

    #region Unity Functions

    #endregion

    #region Public Functions
    public override void onPress()
    {
        Log("Spell Hold");
        m_hit = false;
        if (m_castSound)
        {
            AudioManager.s_instance.PlaySound(m_castSound);
        }
    }
    public override void onHold()
    {
        Log("Spell Hold");
        RaycastHit raycast;
        if (Physics.Raycast(m_origin.position, m_origin.forward, out raycast, Mathf.Infinity, m_LayerMask))
        {
            if (m_target != null)
            {
                m_target.transform.position = m_origin.position + m_origin.forward * raycast.distance + raycast.normal * 0.025f;
                m_target.transform.rotation = Quaternion.LookRotation(raycast.normal, Vector3.back) * Quaternion.Euler(90, 0, 0);
                m_target.SetActive(true);
                if (raycast.distance <= m_Range && Vector3.Dot(raycast.normal, Vector3.up) >= 0.9)
                {
                    m_hit = true;
                    m_target.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.blue);
                    m_hitPos = raycast.point;
                }
                else
                {
                    m_hit = false;
                    m_target.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
                }
            }
        }
        else
        {
            m_hit = false;
            m_target.SetActive(false);
        }

    }
    public override void onRelease()
    {
        Log("Spell Released");
        m_target.SetActive(false);

        if (m_hit)
        {
            if (m_TrapPrefab != null)
            {
                GameObject Trap = Instantiate(m_TrapPrefab, m_hitPos, Quaternion.identity, null);
                if (Trap.GetComponent<TrapObject>())
                {
                    Trap.GetComponent<TrapObject>().Activate();
                }
            }
            if (m_ActivateSound)
            {
                AudioManager.s_instance.PlaySound(m_ActivateSound);
            }
        }
        else
        {
            if (m_failSound)
            {
                AudioManager.s_instance.PlaySound(m_failSound);
            }
        }
    }
    public override void onEquip()
    {
        Log("Spell Unequiped");
        if (m_TargetPrefab != null && m_target == null)
        {
            m_target = Instantiate(m_TargetPrefab, Vector3.zero, Quaternion.identity, null);
            m_target.SetActive(false);
        }
    }
    public override void onUnequip()
    {
        Log("Spell Unequiped");
        if (m_target) Destroy(m_target);
    }
    #endregion

    #region Private Functions
    private void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[Trap][" + _msg + "]");
        }
    }
    private void LogWarning(string _msg)
    {
        if (m_Debug)
        {
            Debug.LogWarning("[Trap][" + _msg + "]");
        }
    }
    #endregion
}
