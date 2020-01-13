using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    [Header("Enemy Attributes")]
    public float m_AttackRate;
    float m_LastAttackTime;
    public float m_Range;

    public virtual void Attack() { }
}
