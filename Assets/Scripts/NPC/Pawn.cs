using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator)), RequireComponent(typeof(CapsuleCollider))]
public class Pawn : MonoBehaviour
{
    [Header("Pawn Attributes")]
    public string m_DebugName;
    public float m_MoveSpeed;
    public float m_TurnSpeed;
    Rigidbody m_Rigidbody;
    Animator m_Animator;

    public void Move(Vector2 direction)
    {
    }
}
