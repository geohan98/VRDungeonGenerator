using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioManager Options")]
    #region Public Variables
    public AudioSource m_AudioSource;
    public static AudioManager s_instance;
    public bool m_Debug = true;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (s_instance == false)
        {
            s_instance = this;
        }
    }
    #endregion

    #region Public Functions
    public void PlaySound(AudioClip _sound)
    {
        if (m_AudioSource)
        {
            m_AudioSource.PlayOneShot(_sound);
        }
    }
    #endregion

    #region Private Functions
    void Log(string _msg)
    {
        if (m_Debug)
        {
            Debug.Log("[AudioManager][" + _msg + "]");
        }
    }
    #endregion
}
