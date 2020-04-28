using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Spells/Trap")]
public class TrapSpell : Spell
{
    #region Public Variables
    [Header("Trap Attributes")]
    public float m_Range;
    public LayerMask m_LayerMask;
    public GameObject m_TargetPrefab;
    public GameObject m_TrapPrefab;
    public bool m_Debug = false;
    [Header("Material")]
    public Texture m_ActiveTarget;
    public Texture m_DisabledTarget;
    #endregion

    #region Private Varaibles
    bool m_hit;
    Vector3 m_hitPos = Vector3.zero;
    GameObject m_target;
    GameObject m_trap;
    #endregion

    #region Public Functions
    public override void onPress()
    {
        m_hit = false;
    }
    public override void onHold()
    {
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
                    m_target.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_ActiveTarget);
                    m_hitPos = raycast.point;
                }
                else
                {
                    m_hit = false;
                    m_target.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_DisabledTarget);
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
        m_target.SetActive(false);

        if (m_hit)
        {
            if (m_TrapPrefab != null)
            {
                if (Time.time - m_lastCastTime >= m_CooldownTime)
                {
                    Instantiate(m_TrapPrefab, m_hitPos, Quaternion.identity, null);
                    m_lastCastTime = Time.time;
                }
            }
        }
    }
    public override void onEquip()
    {
        m_lastCastTime = Time.time;
        if (m_TargetPrefab != null && m_target == null)
        {
            m_target = Instantiate(m_TargetPrefab, Vector3.zero, Quaternion.identity, null);
            m_target.SetActive(false);
        }
    }
    public override void onUnequip()
    {
        if (m_target) Destroy(m_target);
    }
    #endregion
}
