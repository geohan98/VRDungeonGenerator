using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("MeleeEnemy Options")]
    public List<string> m_AttackNames = new List<string>();
    public LayerMask m_HitMask;
    protected override void Attack()
    {
        m_Animator.Play(m_AttackNames[Random.Range(0, m_AttackNames.Count)]);
    }

    public void LandPunch()
    {
        Debug.DrawRay(transform.position + Vector3.up * 1.0f, transform.forward, Color.red, 1);
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up * 1.0f, 0.5f, transform.forward, out hit, 1, m_HitMask))
        {
            PlayerManager player;
            if (player = hit.collider.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>())
            {
                player.Hit();
            }
        }
    }
}
