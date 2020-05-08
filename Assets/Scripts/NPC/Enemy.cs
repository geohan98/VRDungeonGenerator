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
    public LayerMask m_SightMask;
    public float m_EyeHeight;
    public bool m_Debug = true;
    [Header("References")]
    public Rigidbody m_Rigidbody;
    public NavMeshAgent m_Agent;
    public Animator m_Animator;
    public RagdollController m_RagdollController;
    #endregion

    #region Private Variables
    private int m_MaxHealth;
    private float m_LastAttack;
    protected bool m_Alive = true;
    protected Transform m_Target;
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
        if (m_Alive)
        {
            if (Vector3.Distance(transform.position + Vector3.up * m_EyeHeight, PlayerManager.s_Instance.m_Camera.transform.position) <= m_MoveRange && m_Target == null)
            {
                Log("In Range");
                RaycastHit ray;
                if (TargetInSight())
                {
                    Log("In Sight");
                    m_Target = PlayerManager.s_Instance.m_Camera.transform;
                }
            }
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
            {
                if (m_Target)
                {
                    if (TargetInSight())
                    {
                        Log("In Sight");
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
                    }
                    else
                    {
                        Move(m_Target.position);
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
    public float GetHealthPercent()
    {
        float current = m_Health;
        float max = m_MaxHealth;


        return current / max;
    }
    public virtual void Die()
    {
        m_Alive = false;
        m_Agent.enabled = false;
        if (m_RagdollController) m_RagdollController.m_Ragdoll = true;
        LevelController.s_Instance.m_EnemyCount--;
        Destroy(gameObject, 30.0f);
    }
    #endregion

    #region Protected Functions
    protected void Move(Vector3 _position)
    {
        m_Agent.isStopped = false;
        m_Agent.SetDestination(_position);
    }
    protected virtual void Attack() { }
    protected void Stop()
    {
        m_Agent.isStopped = true;
        m_Agent.velocity = Vector3.zero;
    }
    protected void Face(Vector3 _position)
    {
        Vector3 dir = _position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, m_TurnSpeed * Time.deltaTime);
    }
    protected bool TargetInSight()
    {
        Debug.DrawRay(transform.position + Vector3.up * m_EyeHeight, PlayerManager.s_Instance.m_Camera.transform.position - transform.position, Color.red, 0.4f);
        return !Physics.Raycast(transform.position + Vector3.up * m_EyeHeight, PlayerManager.s_Instance.m_Camera.transform.position - transform.position, Vector3.Distance(transform.position + Vector3.up * m_EyeHeight, PlayerManager.s_Instance.m_Camera.transform.position), m_SightMask);
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
