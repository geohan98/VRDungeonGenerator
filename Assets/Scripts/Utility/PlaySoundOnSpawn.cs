using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnSpawn : MonoBehaviour
{
    [Header("PlaySoundOnSpawn Options")]
    #region Public Variables
    public AudioClip m_Sound;
    public bool m_Loop;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (m_Sound)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.loop = m_Loop;
            audio.clip = m_Sound;
            audio.Play();
        }
    }
    #endregion

    #region Public Functions
    public void StopAudio()
    {
        GetComponent<AudioSource>().Stop();
    }
    #endregion
}
