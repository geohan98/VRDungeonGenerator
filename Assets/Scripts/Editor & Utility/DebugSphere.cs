using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    public Vector3 m_offset;
    public float m_radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + m_offset, m_radius);
        Gizmos.DrawWireSphere(transform.position + m_offset + Vector3.up, m_radius);
    }
}
