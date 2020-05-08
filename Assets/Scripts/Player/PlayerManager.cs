using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerManager Options")]
    #region Public Variables
    public static PlayerManager s_Instance;
    public bool m_Alive = true;
    public int m_Heath = 10;
    public int m_RegenRate = 2;
    public float m_RegenCooldown = 5.0f;
    [Header("Scene")]
    public AudioSource m_2DAudioSource;
    public Transform m_Camera;
    public PostProcessVolume m_PostProcessVolume;
    public CapsuleCollider m_CapsuleCollider;
    public bool m_Debug = true;
    #endregion

    #region Private Variables
    private Vignette m_PostProcessVignette;
    private int m_MaxHealth;
    private float m_LastHit;
    private float m_lastRegen;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (s_Instance == null) s_Instance = this;
        if (m_2DAudioSource == null) m_2DAudioSource = GetComponent<AudioSource>();
        if (m_PostProcessVolume) m_PostProcessVolume.profile.TryGetSettings<Vignette>(out m_PostProcessVignette);
        m_PostProcessVignette.color.value = Color.red;
        m_MaxHealth = m_Heath;
        m_lastRegen = Time.time;
        m_LastHit = Time.time;
    }
    private void Update()
    {
        float current = m_Heath;
        float max = m_MaxHealth;
        m_PostProcessVignette.intensity.value = Mathf.Lerp(1.0f, 0.0f, current / max);
        if (m_Alive)
        {

            if (m_Heath < m_MaxHealth)
            {
                if (Time.time - m_LastHit >= m_RegenCooldown)
                {
                    if (Time.time - m_lastRegen >= 1.0f / m_RegenRate)
                    {
                        m_lastRegen = Time.time;
                        m_Heath++;
                        m_Heath = Mathf.Clamp(m_Heath, 0, m_MaxHealth);
                    }
                }
            }
        }
        if (m_CapsuleCollider)
        {
            Vector3 position = m_Camera.position - Vector3.up * ((m_CapsuleCollider.height / 2.0f) * transform.localScale.y - m_CapsuleCollider.radius * transform.localScale.y);
            m_CapsuleCollider.gameObject.transform.position = position;
        }
    }
    #endregion

    #region Public Functions
    public void PlaySoundOneShot(AudioClip _sound)
    {
        if (m_2DAudioSource)
        {
            m_2DAudioSource.PlayOneShot(_sound);
        }
    }
    public Transform GetPlayerTransform() { return gameObject.transform; }
    public void SetPlayerPosition(Vector3 _position)
    {
        transform.position = _position;
    }
    public void SetPlayerRotation(Vector3 _rotation)
    {
        transform.rotation = Quaternion.Euler(_rotation);
    }
    public void Hit()
    {
        m_LastHit = Time.time;
        m_Heath--;
        if (m_Heath <= 0) Die();
    }
    #endregion

    #region Private Functions
    void Die()
    {
        m_Alive = false;
        m_PostProcessVignette.color.value = Color.black;
    }
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[PlayerManager][" + _msg + "]");
        }
    }
    #endregion
}
