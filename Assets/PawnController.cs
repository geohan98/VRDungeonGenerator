using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    [Header("PawnController Options")]
    public RagdollController m_RagdollController;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.collider.gameObject.layer == 12)
        {
            m_RagdollController.EnableRagdoll();
        }
    }
}
