using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Spells/Trap")]
public class Trap : Spell
{
    [Header("Trap Attributes")]
    public float m_range;
    public LayerMask m_LineMask;
    public float m_radius;
    public LayerMask m_sphereMask;
    bool m_hit;
    Vector3 m_hitPos;
    public GameObject m_targetPrefab;
    GameObject m_target;
    public GameObject m_TrapPrefab;

    public override void onPress() { }
    public override void onHold()
    {
        m_hit = false;
        m_target.SetActive(false);
        if (m_range > 0)
        {
            RaycastHit lineHit;
            if (Physics.Raycast(m_origin.position, m_origin.forward, out lineHit, Mathf.Infinity, m_LineMask))
            {
                Debug.DrawLine(m_origin.position, lineHit.point, Color.red);
                if (lineHit.distance <= m_range)
                {
                    m_target.transform.position = lineHit.point;
                    m_target.SetActive(true);
                    Debug.Log(lineHit.collider.gameObject.ToString());
                }
            }
        }
    }
    public override void onRelease()
    {
        Debug.Log(m_hit);
        if (m_hit && m_TrapPrefab != null)
        {
            GameObject tmp = Instantiate(m_TrapPrefab, m_hitPos, Quaternion.Euler(Vector3.zero));
            tmp.GetComponent<TrapObject>().Activate();
        }
    }
    public override void onEquip()
    {
        m_hit = false;
        if (m_targetPrefab != null && m_target == null)
        {
            m_target = Instantiate(m_targetPrefab, Vector3.zero, Quaternion.Euler(Vector3.up), null);
            m_target.SetActive(false);
        }
    }
    public override void onUnequip()
    {
        if (m_target != null)
        {
            Destroy(m_target);
        }
    }
}
