using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerManager Options")]
    #region Public Variables
    public static PlayerManager s_Instance;
    public AudioSource m_2DAudioSource;
    public Transform m_Camera;
    public bool m_Debug = true;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        if (m_2DAudioSource == null)
        {
            m_2DAudioSource = GetComponent<AudioSource>();
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
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[PlayerManager][" + _msg + "]");
        }
    }
    #endregion
}
