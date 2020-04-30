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
    public bool m_HideOnHit = true;
    public GameObject m_HitParticle;
    public bool m_PlayerMade = false;
    public bool m_Debug = true;
    #endregion

    #region Private Variables
    Rigidbody m_RigidBody;
    float m_SpawnTime;
    float m_LifeTime;
    bool m_hit = false;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        m_SpawnTime = Time.time;
        m_LifeTime = m_Range / m_Speed;
        if (!(m_RigidBody = GetComponent<Rigidbody>()))
        {
            Log("Missing Rigidbody");
        }
        m_RigidBody.velocity = transform.forward * m_Speed;
        m_RigidBody.useGravity = m_Gravity;

    }
    private void Update()
    {
        if (Time.time - m_SpawnTime > m_LifeTime && !m_hit)
        {
            Destroy(gameObject, 5.0f);
            gameObject.SetActive(false);
        }
        if (m_AlignWithVelocity && !m_hit)
        {
            transform.forward = m_RigidBody.velocity;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Log("Collsion Detected");
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            if (other.gameObject.layer == 9)
            {
                Log("Projectile Hit NPC");
                gameObject.SetActive(false);
                other.gameObject.GetComponent<Enemy>().Hit();
            }
            if (other.gameObject.layer == 10)
            {
                Log("Projectile Hit Level");
                gameObject.SetActive(!m_HideOnHit);
                m_RigidBody.isKinematic = true;
                m_RigidBody.detectCollisions = false;
                m_RigidBody.useGravity = false;
                m_RigidBody.velocity = Vector3.zero;
            }
            if (m_HitParticle)
            {
                Instantiate(m_HitParticle, transform.position, Quaternion.identity, null);
            }
            Destroy(gameObject, 5.0f);
            m_hit = true;
        }
        else if (other.gameObject.layer == 8)
        {
            if (!m_PlayerMade)
            {
                Log("Projectile Hit Player");
                gameObject.SetActive(false);
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>().Hit();
                if (m_HitParticle)
                {
                    Instantiate(m_HitParticle, transform.position, Quaternion.identity, null);
                }
                Destroy(gameObject, 5.0f);
                m_hit = true;
            }
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
