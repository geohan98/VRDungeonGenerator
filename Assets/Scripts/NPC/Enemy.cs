using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Options")]
    #region Public Variables
    public int m_Health = 4;
    public float m_Speed = 2.5f;
    public float m_TurnSpeed = 360.0f;
    public float m_AttackRate = 1.0f;
    public float m_AttackRange = 1.0f;
    public float m_MoveRange = 1.0f;
    public bool m_Debug = true;
    [Header("References")]
    public Rigidbody m_Rigidbody;
    public NavMeshAgent m_Agent;
    public Animator m_Animator;
    #endregion

    #region Private Variables
    private int m_MaxHealth;
    private float m_LastAttack;
    [SerializeField] private Transform m_Target;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        m_MaxHealth = m_Health;
        m_Agent.speed = m_Speed;
        m_Agent.angularSpeed = m_TurnSpeed;
    }
    private void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            if (m_Target)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(m_Target.position.x, 0, m_Target.position.z)) < m_AttackRange)
                {
                    Stop();
                }
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(m_Target.position.x, 0, m_Target.position.z)) < m_MoveRange)
                {
                    Face(m_Target.position);
                    Vector3 target = m_Target.position - transform.position;
                    target.y = 0;
                    Vector3 forward = transform.forward;
                    forward.y = 0;
                    if (Vector3.Angle(target, forward) < 5.0f)
                    {
                        if (Time.time - m_LastAttack > 1.0f / m_AttackRate)
                        {
                            Attack();

                            m_LastAttack = Time.time;
                        }
                    }
                }
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(m_Target.position.x, 0, m_Target.position.z)) > m_MoveRange)
                {
                    Move(m_Target.position);
                }
            }
        }
        else
        {
            Stop();
        }
        m_Animator.SetFloat("Speed", Vector3.Magnitude(m_Agent.velocity) / m_Speed);
    }
    #endregion

    #region Public Functions
    public void Hit()
    {
        m_Animator.Play("Hit");
        m_Health--;
        if (m_Health <= 0)
        {
            Die();
        }
    }
    #endregion

    #region Protected Functions
    protected void Move(Vector3 _position)
    {
        m_Agent.isStopped = false;
        m_Agent.SetDestination(_position);
    }
    protected virtual void Attack() { }
    protected virtual void Die() { }
    protected void Stop()
    {
        m_Agent.isStopped = true;
    }
    protected void Face(Vector3 _position)
    {
        Vector3 dir = _position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, m_TurnSpeed * Time.deltaTime);
    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[Enemy][" + _msg + "]");
        }
    }
    #endregion
}
