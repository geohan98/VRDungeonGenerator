using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grab", menuName = "Spells/Grab")]
public class Grab : Spell
{
    [Header("Grab Options")]
    #region Public Variables
    public float m_Range = 10;
    public LayerMask m_RaycastMask;
    public bool m_Debug = true;
    #endregion

    #region Private Variables
    private GameObject m_Target;
    private Rigidbody m_TargetRigidbody;
    #endregion

    #region Public Functions
    public override void onPress()
    {
        RaycastHit raycast;
        if (Physics.Raycast(m_origin.position, m_origin.forward, out raycast, m_Range, m_RaycastMask))
        {
            if (raycast.collider.gameObject.layer == 12 && raycast.collider.gameObject.GetComponent<Rigidbody>())
            {
                m_Target = raycast.collider.gameObject;
                m_TargetRigidbody = m_Target.GetComponent<Rigidbody>();
                m_TargetRigidbody.useGravity = false;
                m_TargetRigidbody.velocity = Vector3.zero;
            }
        }
    }

    public override void onHold()
    {
        if (m_Target && m_TargetRigidbody)
        {

            Vector3 distance = m_Target.transform.position - m_origin.position;
            distance *= -1;
            Vector3 targetVel = Vector3.ClampMagnitude(20.0f * distance, 10.0f);
            Vector3 error = targetVel - m_TargetRigidbody.velocity;
            Vector3 force = Vector3.ClampMagnitude(20.0f * error, 200.0f);


            m_TargetRigidbody.AddForce(force);
        }
    }
    public override void onRelease()
    {
        if (m_Target)
        {
            m_Target = null;
        }
        if (m_TargetRigidbody)
        {
            m_TargetRigidbody.useGravity = true;
            m_TargetRigidbody = null;
        }
    }
    public override void onEquip()
    {

    }
    public override void onUnequip()
    {

    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[Grab][" + _msg + "]");
        }
    }
    #endregion
}
