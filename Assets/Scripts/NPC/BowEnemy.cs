using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : Enemy
{

    [Header("MeleeEnemy Options")]
    public List<string> m_AttackNames = new List<string>();
    public GameObject m_ArrowPrefab;
    public Transform m_ArrowSpawnPoint;
    protected override void Attack()
    {
        m_Animator.Play(m_AttackNames[Random.Range(0, m_AttackNames.Count)]);
    }
    public void FireArrow()
    {
        if (m_ArrowPrefab && m_ArrowSpawnPoint) Instantiate(m_ArrowPrefab, m_ArrowSpawnPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up), null);
    }
}
