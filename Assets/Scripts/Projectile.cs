using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("MissleProjectile Options")]
    #region Public Variables
    public float m_Speed = 2.0f;
    public float m_Range = 10.0f;
    public bool m_Gravity = false;
    public bool m_AlignWithVelocity = false;
    public GameObject m_HitParticle;
    public bool m_Debug = true;
    #endregion

    #region Private Variables
    Rigidbody m_RigidBody;
    float m_SpawnTime;
    float m_LifeTime;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        m_SpawnTime = Time.time;
        m_LifeTime = m_Range - m_Speed;
        if (!(m_RigidBody = GetComponent<Rigidbody>()))
        {
            Log("Missing Rigidbody");
        }
        m_RigidBody.velocity = transform.forward * m_Speed;
        if (m_Gravity)
        {
            m_RigidBody.useGravity = true;
        }
    }
    private void Update()
    {
        if (Time.time - m_SpawnTime > m_LifeTime)
        {
            Destroy(gameObject, 5.0f);
            gameObject.SetActive(false);
        }
        if (m_AlignWithVelocity)
        {
            transform.forward = m_RigidBody.velocity;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Log("Collsion Detected");
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            if (other.gameObject.layer == 8)
            {
                Log("Projectile Hit Player");
            }
            if (other.gameObject.layer == 9)
            {
                Log("Projectile Hit NPC");
            }
            if (m_HitParticle)
            {
                Instantiate(m_HitParticle, transform.position, Quaternion.identity, null);
            }
            Destroy(gameObject, 5.0f);
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region Public Functions
    public void init(float _range = 10.0f, float _speed = 2.0f)
    {
        m_Speed = _speed;
        m_LifeTime = _range / _speed;
        m_RigidBody.velocity = transform.forward * m_Speed;
    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[MissleProjectile][" + _msg + "]");
        }
    }
    #endregion
}
