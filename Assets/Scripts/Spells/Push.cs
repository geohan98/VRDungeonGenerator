using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Push", menuName = "Spells/Push")]
public class Push : Spell
{
    [Header("Push Options")]
    #region Public Variables
    public float m_Range = 10.0f;
    public float m_Force = 10.0f;
    public LayerMask m_RaycastMask;
    public bool m_Debug = true;
    #endregion

    #region Public Functions
    public override void onPress()
    {
        GameObject Target;
        Rigidbody TargetRigidbody;

        RaycastHit raycast;
        if (Physics.Raycast(m_origin.position, m_origin.forward, out raycast, m_Range, m_RaycastMask))
        {
            if ((raycast.collider.gameObject.layer == 12 || raycast.collider.gameObject.layer == 13) && raycast.collider.gameObject.GetComponent<Rigidbody>())
            {
                Target = raycast.collider.gameObject;
                TargetRigidbody = Target.GetComponent<Rigidbody>();
                TargetRigidbody.velocity = Vector3.zero;
                TargetRigidbody.AddForce(m_origin.forward * m_Force, ForceMode.Impulse);
            }
        }
    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[Push][" + _msg + "]");
        }
    }
    #endregion
}
