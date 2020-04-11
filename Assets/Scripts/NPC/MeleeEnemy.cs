using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("MeleeEnemy Options")]
    public List<string> m_AttackNames = new List<string>();
    protected override void Attack()
    {
        m_Animator.Play(m_AttackNames[Random.Range(0, m_AttackNames.Count)]);
    }
}
