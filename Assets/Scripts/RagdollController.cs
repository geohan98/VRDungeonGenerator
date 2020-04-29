using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [Header("RagdollController Options")]
    #region Public Variables
    public bool m_Ragdoll = false;
    public Rigidbody m_Root;
    public Animator m_AnimController;
    public List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();
    public List<Collider> m_Colliders = new List<Collider>();
    public bool m_Debug = true;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (!m_Root) m_Root = GetComponent<Rigidbody>();
        if (!m_AnimController) m_AnimController = GetComponent<Animator>();
        m_Rigidbodies = new List<Rigidbody>();
        m_Colliders = new List<Collider>();
        GetRigidBodies(transform);
        DisableRagdoll();
    }
    private void Update()
    {
        if (m_Ragdoll)
        {
            EnableRagdoll();
            m_Root.position = m_Rigidbodies[0].worldCenterOfMass;
        }
        else
        {
            DisableRagdoll();
        }
    }
    #endregion

    #region Public Functions

    #endregion

    #region Private Functions
    private void GetRigidBodies(Transform _root)
    {
        foreach (Transform child in _root)
        {
            if (child.GetComponent<Rigidbody>())
            {
                m_Rigidbodies.Add(child.GetComponent<Rigidbody>());
                m_Colliders.Add(child.GetComponent<Collider>());
            }

            GetRigidBodies(child);
        }
    }
    public void DisableRagdoll()
    {
        m_Ragdoll = false;
        m_Root.detectCollisions = true;
        m_Root.isKinematic = false;
        m_AnimController.enabled = true;
        foreach (Rigidbody rigidbody in m_Rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
        foreach (Collider collider in m_Colliders)
        {
            collider.enabled = false;
        }
    }
    public void EnableRagdoll()
    {
        m_Ragdoll = true;
        m_Root.detectCollisions = false;
        m_Root.isKinematic = true;
        m_AnimController.enabled = false;
        foreach (Rigidbody rigidbody in m_Rigidbodies)
        {
            rigidbody.detectCollisions = true;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
        foreach (Collider collider in m_Colliders)
        {
            collider.enabled = true;
        }
    }
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[RagdollController][" + _msg + "]");
        }
    }
    #endregion
}
